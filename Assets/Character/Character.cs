using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public abstract class Character : Entity
{
    // ARCHITECTURE
    public QuestManager _questManager;
    public ScreenManager _screenManager;
    public GameManager _gameManager;
    Formation _formation;

    // PHYSICAL
    NavMeshAgent _agent;
    Rigidbody _rigidBody;

    public GameObject _handRight;
    public GameObject _handLeft;
    public Weapon _weapon;

    // DYNAMICS
    eCharacterAnimation _animation;
    
    // DATA
    public CharacterData _data;
    // STATUS
    public bool _isAlive;
    public float _baseHp;
    public float _currentHp;
    float _baseMoveSpeed;
    protected List<Ability> _abilities;

    public Action _jobAbility;
    List<Effect> _effects;


    #region INITIATE
    public void SetCharacter(GameManager gameManager, ScreenManager screenManager, Formation formation, CharacterData data)
    {
        _rigidBody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _isAlive = true;
        _animation = eCharacterAnimation.Standing;

        _gameManager = gameManager;
        _questManager = _gameManager._questManager;
        _screenManager = screenManager;
        _formation = formation;
        _data = data;
        _effects = new List<Effect>();

        _baseHp = _data._baseHp;
        _baseMoveSpeed = _data._baseMoveSpeed;
        _currentHp = _baseHp;
        InstantiateAbilities(_data);

        AbstractInitialize();
        SetMoveStop();
    }

    protected abstract void AbstractInitialize();


    void InstantiateAbilities(CharacterData data)
    {
        _abilities = new List<Ability>();
        foreach (AbilityData ability in data._baseAbilities)
            InstantiateAbility(ability);
    }
    void InstantiateAbility(AbilityData ability)
    {
        if (ability is ActionData action)
        {
            Action a = Instantiate(_gameManager._prefabs._action, transform);
            a.SetAction(this, action);
            _abilities.Add(a);
        }
        else if (ability is ReactionData reaction)
        {
            Reaction r = Instantiate(_gameManager._prefabs._reaction, transform);
            r.SetReaction(this, reaction);
            _abilities.Add(r);
        }
    }
    #endregion
    

    float GetDuration(AbilityEffect effect)
    {
        return effect._duration;
    }
    float GetQuantity(AbilityEffect effect)
    {
        float multiplier = 1.0f;
        if (effect._type == eEffect.Damage)
        {
            List<Effect> damageBuffs = _effects.Where(x => x._effect._type == eEffect.DamageBuff).ToList();
            foreach (Effect buff in damageBuffs)
                multiplier += buff._effect._quantity;
        }
        return multiplier * effect._quantity;
    }
    public AbilityEffect GetEffect(AbilityEffect effect)
    {
        return new AbilityEffect
        {
            _type = effect._type,
            _duration = GetDuration(effect),
            _quantity = GetQuantity(effect)
        };
    }
    public void EndureEffects()
    {
        List<Effect> effects = _effects.ToList();
        foreach (Effect effect in effects)
        {
            eEffect type = effect._effect._type;
            if (type == eEffect.Damage)
                TakeDamage(effect._effect._quantity);
            else if (type == eEffect.Heal)
                HealDamage(effect._effect._quantity);

            if (effect.IsFinished())
                _effects.Remove(effect);
        }
    }
    public void AddEffect(Effect effect)
    {
        _effects.Add(effect);
    }
    
    bool SameTeam(Character otherCharacter)
    {
        return _data._owner._teamNumber == otherCharacter._data._owner._teamNumber;
    }
    public bool IsTargetableTarget(Ability ability)
    {
        List<eTarget> relations = GetRelations(ability._character);

        return relations.Contains(ability._abilityData._targetting._target);
    }
    public bool IsVisibleToCharacter(Character otherCharacter)
    {
        return (SameTeam(otherCharacter) || !_effects.Select(x => x._effect._type).Contains(eEffect.Invisible));
    }
    public bool IsTauntingCharacter(Character character)
    {
        List<Effect> tauntEffects = character._effects.Where(x => x._effect._type == eEffect.Taunt).ToList();
        if (tauntEffects.Count == 0)
            return true; // IF this character is not taunted, THEN all characters are targetable
        else
            return tauntEffects.Where(x => x._effector == this).ToList().Count > 0;
    }
    public bool IsAtMaxHp()
    {
        return _currentHp == _baseHp;
    }


    List<Action> GetOffCooldown(List<Action> abilities)
    {
        return abilities.Where(x => x.IsOffCoolDown()).ToList();
    }
    List<Action> GetTargetable(List<Action> abilities)
    {
        return abilities.Where(x => x.GetClosestTarget() != null).ToList();
    }
    List<Action> GetInRange(List<Action> abilities)
    {
        return abilities.Where(x => x.TargetIsInRange(x.GetClosestTarget())).ToList();
    }
    

    public List<eTarget> GetRelations(Character character)
    {
        if (character == null)
            return new List<eTarget>();

        List<eTarget> relations = new List<eTarget> { eTarget.Any };
        if (character == this)
            relations.AddRange(new List<eTarget> { eTarget.Self, eTarget.AllyAny });
        else if (SameTeam(character))
            relations.AddRange(new List<eTarget> { eTarget.AllyOther, eTarget.AllyAny });
        else
            relations.Add(eTarget.Enemy);

        return relations;
    }


    protected override void TakeTick()
    {
        if (_jobAbility == null ||
            (_jobAbility._abilityData._weapons._thrown && (_jobAbility._state == eActionState.Act || _jobAbility._state == eActionState.Lag)))
        {
            if (_weapon != null)
            {
                Destroy(_weapon.gameObject);
                _weapon = null;
            }
        }
        else if (_weapon == null)
        {
            _weapon = Instantiate(_gameManager._prefabs._weapons._sword, _handRight.transform);
        }

        EndureEffects();

        if (_currentHp <= 0)
            KillCharacter();
        if (!_isAlive)
            return;

        UpdateMoveSpeed();

        Agency();

        ManageWalkAnimation();
    }

    void Agency()
    {
        if (_formation._state != eFormation.Engage)
            return;


        if (IsStunned())
        {
            SetMoveStop();
        }
        else
        {
            if (IsActing())
            {
                // Continue Acting
            }
            else
            {
                List<Action> readied = GetReadiedActions();
                List<Action> targetable = GetTargetable(readied);
                List<Action> offCd = GetOffCooldown(targetable);
                List<Action> inRange = GetInRange(offCd);

                if (inRange.Count > 0)
                {
                    // IF WE CAN DO A MOVE
                    SetMoveStop();
                    if (IsMute())
                    {

                    }
                    else
                    {
                        _jobAbility = inRange[0];
                        _jobAbility.Trigger();
                    }
                }
                else
                {
                    // IF WE CANT DO A MOVE
                    if (IsRooted())
                    {
                        SetMoveStop();
                    }
                    else
                    {
                        List<Action> considered = GetNonZeroList(new List<List<Action>> { offCd, targetable, readied});

                        if (considered.Count == 0)
                            SetMoveStop();
                        else
                            HandleMove(considered[0]);

                    }
                }
            }
        }
    }
    bool IsRooted()
    {
        return _effects.Select(x => x._effect._type).Contains(eEffect.Root);
    }
    bool IsStunned()
    {
        return _effects.Select(x => x._effect._type).Contains(eEffect.Stun);
    }
    bool IsMute()
    {
        return _effects.Select(x => x._effect._type).Contains(eEffect.Mute);
    }
    bool IsActing()
    {
        List<eActionState> actionStates = new List<eActionState> { eActionState.Prepare, eActionState.Act, eActionState.Lag };
        return _jobAbility != null && actionStates.Contains(_jobAbility._state);
    }
    void HandleMove(Action moveAction)
    {
        Character target = moveAction.GetClosestTarget();
        if (target != null && !moveAction.TargetIsInRange(target))
            SetMoveTarget(target.transform.position);
        else
            SetMoveStop();
    }

    List<Action> GetNonZeroList(List<List<Action>> actions)
    {
        foreach (List<Action> actionList in actions)
        {
            if (actionList.Count != 0)
                return actionList;
        }

        return actions[actions.Count - 1];
    }
    
    void UpdateMoveSpeed()
    {
        _agent.speed = 3.5f * _screenManager._timer.timeScale * _baseMoveSpeed;
    }

    List<Action> GetReadiedActions()
    {
        List<Action> actions = new List<Action>();
        foreach (Ability ability in _abilities)
            if (ability is Action action && action._state == eActionState.Ready)
                actions.Add(action);
        
        return actions;
    }
    public override void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
        foreach (Ability ability in _abilities)
            ability.ReceiveEvent(ge);
    }


    // MOVEMENT
    public void SetMoveTarget(Vector3 target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target);
    }
    public void SetMoveStop()
    {
        _agent.SetDestination(this.transform.position);
        _agent.isStopped = true;
    }
    // DAMAGE
    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        StartCoroutine(TakeDamage());
    }
    public void HealDamage(float damage)
    {
        _currentHp += damage;
        _currentHp = Mathf.Min(_currentHp, _baseHp);
        StartCoroutine(HealDamage());
    }

    protected abstract IEnumerator TakeDamage();
    protected abstract IEnumerator HealDamage();

    void KillCharacter()
    {
        _isAlive = false;
        gameObject.SetActive(false);
    }
    // ANIMATIONS
    void ManageWalkAnimation()
    {
        if (!_isAlive)
            return;

        if (_animation == eCharacterAnimation.Standing)
        {
            if (!_agent.isStopped)
            {
                _animation = eCharacterAnimation.Walking;
                StartCoroutine(WalkAnimation());
            }
        }
        else if (_animation == eCharacterAnimation.Walking)
        {
            if (_agent.isStopped)
            {
                _animation = eCharacterAnimation.Standing;
                StopAllCoroutines();
                PoseNeutral();
            }
        }
    }


    #region ANIMATIONS
    IEnumerator WalkAnimation()
    {
        while (true)
        {
            PoseWalkLeft();
            float endTime = _screenManager._timer.time + 0.2f;
            while (_screenManager._timer.time < endTime)
                yield return null;


            PoseNeutral();
            endTime = _screenManager._timer.time + 0.1f;
            while (_screenManager._timer.time < endTime)
                yield return null;

            PoseWalkRight();
            endTime = _screenManager._timer.time + 0.2f;
            while (_screenManager._timer.time < endTime)
                yield return null;

            PoseNeutral();
            endTime = _screenManager._timer.time + 0.1f;
            while (_screenManager._timer.time < endTime)
                yield return null;
        }
    }
    public abstract void PoseNeutral();
    public abstract void PoseWalkRight();
    public abstract void PoseWalkLeft();
    public abstract void PoseMeleeStart();
    public abstract void PoseMeleeStrike();
    public abstract void PoseMeleeFinish();
    #endregion
}
