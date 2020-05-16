using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Effect : QuestEventHandlerData
{
    public AbilityEffect _effect;
    public AbilityEffect _originalEffect;
    public Character _effector;
    public float _endTime;

    public Effect(Character effector, AbilityEffect effect)
    {
        _effector = effector;
        _originalEffect = effect;
        _effect = _effector.GetEffect(effect);
        _endTime = _effector._screenManager._timer.time + _effect._duration;
    }

    public bool IsFinished()
    {
        return _endTime <= _effector._screenManager._timer.time;
    }

    public override void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
    }
    protected override void HandleEventAbility(QuestEventAbility gea)
    {
        //
    }
    protected override void HandleEventEffect(QuestEventEffect gea)
    {
        //
    }

}