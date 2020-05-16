using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum eEffect { Damage, Invisible, Taunt, DamageBuff, Heal, Stun, Root, Mute, Move }
public class AbilityEffect
{
    public float _duration;
    public float _quantity;
    public eEffect _type;
}


