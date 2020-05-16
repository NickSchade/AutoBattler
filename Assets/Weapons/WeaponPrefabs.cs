using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPrefabs : MonoBehaviour
{
    public Weapon _sword;
    public Weapon _dagger;

    public Weapon GetWeapon(eWeapon weapon)
    {
        if (weapon == eWeapon.Sword)
            return _sword;
        else if (weapon == eWeapon.Dagger)
            return _dagger;
        else
            throw new System.NotImplementedException();
    }

}
