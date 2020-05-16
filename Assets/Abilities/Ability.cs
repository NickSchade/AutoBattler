using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Ability : MonoBehaviour, IQuestEventHandler
{
    public float _stateStartTime;
    public float _nextStateTime;

    public float _offCooldownTime;

    public AbilityData _abilityData;
    public Character _character;
    public QuestEventAbility _event;

    public void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }
    public abstract void HandleEventAbility(QuestEventAbility gea);
    public abstract void HandleEventEffect(QuestEventEffect gea);
    public abstract void ReceiveEvent(GameEvent ge);

    public void Trigger(Character target)
    {
        foreach (AbilityEffect effect in _abilityData._effects)
        {
            QuestEventEffect gee = GetEffect(target, effect);
            _character._questManager.ReceiveEvent(gee);
        }
    }
    public QuestEventEffect GetEffect(Character target, AbilityEffect effect)
    {
        return new QuestEventEffect { _actor = _character, _ability = this, _effect = effect, _target = target };
    }


    public bool TargetIsInRange(Character target)
    {
        if (target == null)
            return false;

        return _abilityData._targetting._range > Vector3.Distance(transform.position, target.transform.position);
    }


    public bool IsOffCoolDown()
    {
        return _offCooldownTime < _character._screenManager._timer.time;
    }



    protected List<Character> GetValidTargetsInAoe(Vector3 target)
    {
        List<Character> validTargets = GetTargetableCharacters();
        return validTargets.Where(x => Vector3.Distance(target, x.transform.position) <= _abilityData._targetting._aoe).ToList();
    }


    public Character GetClosestTarget()
    {
        List<Character> targetableCharacters = GetTargetableCharacters();
        if (targetableCharacters.Count == 0)
            return null;

        return GetClosestTarget(targetableCharacters); // in GetClosestTarget()
    }
    Character GetClosestTarget(List<Character> targetableCharacters)
    {
        float minDistance = targetableCharacters.Min(x => Vector3.Distance(this.transform.position, x.transform.position));
        return targetableCharacters.Where(y => Vector3.Distance(this.transform.position, y.transform.position) == minDistance).ToList()[0];
    }


    List<Character> GetTargetableCharacters()
    {
        return _character._screenManager._characters.Where(x => CanTarget(x)).ToList();
    }

    public bool CanTarget(Character target)
    {
        return target.gameObject.activeSelf
            && target.IsTargetableTarget(this)
            && target.IsVisibleToCharacter(_character)
            && target.IsTauntingCharacter(_character)
            && CanHealTarget(target);
    }

    public bool CanHealTarget(Character target)
    {
        return !_abilityData._effects.Select(x => x._type).Contains(eEffect.Heal) || !target.IsAtMaxHp();
    }
}
