using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    
}

public class QuestEventAbility : GameEvent
{
    public Character _actor;
    public eActionState _state;
    public Ability _ability;
    public Vector3 _target;
    public List<Character> _targets;
}

public class QuestEventEffect : GameEvent
{
    public Character _actor;
    public Ability _ability;
    public AbilityEffect _effect;
    public Character _target;
}

