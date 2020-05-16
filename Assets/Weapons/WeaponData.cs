using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public eWeapon _weapon;
    public eWeapon _projectile;
    public bool _held;
    public bool _thrown;
    public WeaponData(eWeapon weapon, eWeapon projectile = eWeapon.None, bool held = true, bool thrown = false)
    {
        _weapon = weapon;
        _projectile = projectile;
        _held = held;
        _thrown = thrown;
    }
        
}
