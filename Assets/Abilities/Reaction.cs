using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Reaction : Ability
{
    ReactionData _reactionData;
    public void SetReaction(Character character, ReactionData data)
    {
        _character = character;
        _reactionData = data;
        _abilityData = data;
    }
    public override void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
    }

    public override void HandleEventAbility(QuestEventAbility gea)
    {
        if (IsOffCoolDown() && IsTriggered(gea))
        {
            _offCooldownTime = _character._screenManager._timer.time + _abilityData._coolDownTime;
            _event = GetEvent(gea, eActionState.Instant);
            _character._questManager.ReceiveEvent(_event);
            StartCoroutine(React(gea));
        }
    }

    bool IsTriggered(QuestEventAbility gea)
    {
        if (_reactionData._condition is AbilityReactionCondition ar)
        {
            if (!ar._states.Contains(gea._state))
                return false;

            if (ar._effects.Intersect(gea._ability._abilityData._effects.Select(x => x._type)).ToList().Count == 0)
                return false;

            if (ar._reactorToActor.Intersect(_character.GetRelations(gea._actor)).ToList().Count == 0)
                return false;

            bool atLeastOneReactorToTarget = false;
            foreach (Character c in gea._targets)
                if (ar._reactorToTarget.Intersect(_character.GetRelations(c)).ToList().Count > 0)
                    atLeastOneReactorToTarget = true;

            if (!atLeastOneReactorToTarget)
                return false;

            return true;
        }
        else
        {
            throw new System.NotImplementedException();
        }
        
    }

    public override void HandleEventEffect(QuestEventEffect gea)
    {
    }

    List<Character> GetTargets(QuestEventAbility gea)
    {
        if (_reactionData._condition is AbilityReactionCondition ar)
        {
            if (ar._reactionTarget == eAbilityReactionTarget.Self)
                return new List<Character> { this._character };
            else if (ar._reactionTarget == eAbilityReactionTarget.Actor)
                return new List<Character> { gea._actor };
            else if (ar._reactionTarget == eAbilityReactionTarget.AllTargets)
                return gea._targets;
            else if (ar._reactionTarget == eAbilityReactionTarget.PrimaryTarget)
                return new List<Character> { gea._targets[0] };
            else
                throw new System.NotImplementedException();
        }
        else
        {
            return null;
        }
    }

    IEnumerator React(QuestEventAbility gea)
    {
        _character._screenManager._timer.timeScale = 0f;
        _character._screenManager._cinematics._light.color = Color.red;
        yield return new WaitForSeconds(3f);
        _character._screenManager._cinematics._light.color = _character._screenManager._cinematics._startingLightColor;
        _character._screenManager._timer.timeScale = 1f;

        _event = GetEvent(gea, eActionState.Act);
        _character._questManager.ReceiveEvent(_event);

        yield return null;

        _event = GetEvent(gea, eActionState.Lag);
        _character._questManager.ReceiveEvent(_event);

        yield return null;
    }
    QuestEventAbility GetEvent(QuestEventAbility gea, eActionState state)
    {
        List<Character> reactionTargets = GetTargets(gea);
        return GetEvent(reactionTargets, state);
    }
    QuestEventAbility GetEvent(List<Character> targets, eActionState state)
    {
        Vector3 targetPos = targets[0].transform.position;
        List<Character> hitCharacters = new List<Character>();
        foreach (Character target in targets)
            hitCharacters.AddRange(GetValidTargetsInAoe(target.transform.position));

        QuestEventAbility ge = new QuestEventAbility { _actor = _character, _ability = this, _state = state, _target = targetPos, _targets = hitCharacters };
        return ge;
    }

}
