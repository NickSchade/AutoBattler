using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public enum eTarget { Self, AllyOther, AllyAny, Enemy, Any};
public enum eAbilityReactionTarget { Self, Actor, PrimaryTarget, AllTargets};

public class AbilityData
{
    public string _name;
    public float _energyCost;
    public float _coolDownTime;
    public AbilityTargetingData _targetting;
    public List<AbilityEffect> _effects;
    public WeaponData _weapons;
}
public class ActionData : AbilityData
{
    public AbilityDurationData _duration;
}
public class ReactionData : AbilityData
{
    public ReactionCondition _condition;
}
