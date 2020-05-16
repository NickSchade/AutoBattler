using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Action : Ability
{
    public eActionState _state;
    public Vector3 _nextTarget;
    public ActionData _actionData;

    public void SetAction(Character character, ActionData abilityData)
    {
        _character = character;
        _offCooldownTime = _character._screenManager._timer.time;
        _abilityData = abilityData;
        _actionData = abilityData;
        _state = eActionState.Ready;
    }


    public void Trigger()
    {
        _offCooldownTime = _character._screenManager._timer.time + _abilityData._coolDownTime;
        SetPrepareAction();  
    }

    private void Update()
    {
        float currentTime = _character._screenManager._timer.time;
        if (_state == eActionState.Prepare && _nextStateTime < currentTime)
            SetTakeAction();
        else if (_state == eActionState.Act && _nextStateTime < currentTime)
            SetFollowthroughAction();
        else if (_state == eActionState.Lag && _nextStateTime < currentTime)
            SetReadyAction();
    }

    void SetPrepareAction()
    {
        _character.PoseMeleeStart();
        _state = eActionState.Prepare;
        _stateStartTime = _character._screenManager._timer.time;
        _nextStateTime = _stateStartTime + _actionData._duration._prepTime;

        _event = GetEvent();
        _character._questManager.ReceiveEvent(_event);
    }
    QuestEventAbility GetEvent()
    {
        Character target = GetClosestTarget(); // in GetEvent()
        List<Character> hitCharacters = new List<Character> { target };
        Vector3 targetpos = _character.transform.position;
        if (target != null)
        {
            targetpos = target.transform.position;
            hitCharacters = GetValidTargetsInAoe(targetpos);  // in GetEvent()
        }

        QuestEventAbility ge = new QuestEventAbility { _actor = _character, _ability = this, _state = _state, _target = targetpos, _targets = hitCharacters };
        return ge;
    }
    void SetTakeAction()
    {
        _character.PoseMeleeStrike();
        _state = eActionState.Act;
        _stateStartTime = _character._screenManager._timer.time;
        _nextStateTime = _stateStartTime + _actionData._duration._activeTime;

        _event = GetEvent();
        _character._questManager.ReceiveEvent(_event);
    }
    void SetFollowthroughAction()
    {
        _character.PoseMeleeFinish();
        _state = eActionState.Lag;
        _stateStartTime = _character._screenManager._timer.time;
        _nextStateTime = _stateStartTime + _actionData._duration._lagTime;

        _event = GetEvent();
        _character._questManager.ReceiveEvent(_event);
    }
    void SetReadyAction()
    {
        _character.PoseNeutral();
        _character._jobAbility = null;
        _state = eActionState.Ready;

        _event = GetEvent();
        _character._questManager.ReceiveEvent(_event);
    }

    

    public override void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
    }

    public override void HandleEventAbility(QuestEventAbility gea)
    {
        //
    }

    public override void HandleEventEffect(QuestEventEffect gea)
    {
        //
    }
}
