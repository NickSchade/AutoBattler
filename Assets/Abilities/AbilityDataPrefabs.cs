using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AbilityDataPrefabs
{
    public static AbilityData TankSword()
    {
        return new ActionData
        {
            _name = "Sword",
            _coolDownTime = 1.0f,
            _duration = new AbilityDurationData { _prepTime = 0.2f, _activeTime = 0.1f, _lagTime = 0.2f },
            _targetting = new AbilityTargetingData { _range = 1f, _aoe = 0.1f, _target = eTarget.Enemy },
            _effects = new List<AbilityEffect> { new AbilityEffect { _quantity = 1.0f, _duration = 0f, _type = eEffect.Damage } },
            _weapons = new WeaponData(eWeapon.Sword)
        };
    }
    public static AbilityData ThrowDagger()
    {
        return new ActionData
        {
            _name = "Throw Dagger",
            _coolDownTime = 1f,
            _duration = new AbilityDurationData { _prepTime = 0.2f, _activeTime = 0.1f, _lagTime = 0.1f },
            _targetting = new AbilityTargetingData { _range = 10f, _aoe = 0.1f, _target = eTarget.Enemy },
            _effects = new List<AbilityEffect> { new AbilityEffect { _type = eEffect.Damage, _quantity = 2f, _duration = 0f } },
            _weapons = new WeaponData(eWeapon.Dagger, eWeapon.Dagger, thrown:true)
        };
    }
    public static AbilityData AgilityHeroRoot()
    {
        return new ActionData
        {
            _name = "Root",
            _coolDownTime = 5f,
            _duration = new AbilityDurationData { _prepTime = 0.5f, _activeTime = 0.1f, _lagTime = 0.3f },
            _targetting = new AbilityTargetingData { _range = 11f, _aoe = 0.1f, _target = eTarget.Enemy },
            _effects = new List<AbilityEffect> { new AbilityEffect { _type = eEffect.Root, _quantity = 2f, _duration = 3f } },
            _weapons = new WeaponData(eWeapon.None)
        };
    }

    public static AbilityData AgilityHeroDisengage()
    {
        return new ReactionData
        {
            _name = "Disengage",
            _coolDownTime = 10f,
            _targetting = new AbilityTargetingData { _range = 0.5f, _aoe = 0.1f, _target = eTarget.Enemy },
            _effects = new List<AbilityEffect> { new AbilityEffect { _type = eEffect.Move, _duration = 0f, _quantity = 1f } },
            _condition = new AbilityReactionCondition(new List<eActionState> { eActionState.Prepare }, new List<eEffect> { eEffect.Damage }, new List<eTarget> { eTarget.Enemy }, new List<eTarget> { eTarget.Self }, eAbilityReactionTarget.Actor),
            _weapons = new WeaponData(eWeapon.None)
        };
    }

    public static AbilityData TestRetribution()
    {
        return new ReactionData
        {
            _name = "Retribution",
            _coolDownTime = 10f,
            _targetting = new AbilityTargetingData { _range = 0.5f, _aoe = 0.1f, _target = eTarget.Enemy },
            _effects = new List<AbilityEffect> { new AbilityEffect { _type = eEffect.Damage, _duration = 0f, _quantity = 10f } },
            _condition = new AbilityReactionCondition(new List<eActionState> { eActionState.Lag }, new List<eEffect> { eEffect.Damage }, new List<eTarget> { eTarget.Enemy }, new List<eTarget> { eTarget.Self }, eAbilityReactionTarget.Actor),
            _weapons = new WeaponData(eWeapon.None)
        };
    }

    public static AbilityData HealerHeal()
    {
        return new ActionData
        {
            _name = "Heal",
            _coolDownTime = 5f,
            _duration = new AbilityDurationData { _prepTime = 0.2f, _activeTime = 0.1f, _lagTime = 0.2f },
            _targetting = new AbilityTargetingData { _range = 1f, _aoe = 0.1f, _target = eTarget.AllyAny },
            _effects = new List<AbilityEffect> { new AbilityEffect { _type = eEffect.Heal, _quantity = 5f, _duration = 0f } },
            _weapons = new WeaponData(eWeapon.None)
        };
    }
  

    

}
