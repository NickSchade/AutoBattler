using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData
{
    public Player _owner;
    public string _name;
    public float _baseHp;
    public float _baseMoveSpeed;
    public List<AbilityData> _baseAbilities;


    public static CharacterData AgilityHero()
    {
        return new CharacterData
        {
            _name = "AgilityHero",
            _baseHp = 10f,
            _baseMoveSpeed = 1f,
            //_baseAbilities = new List<AbilityData> { AbilityDataPrefabs.AgilityHeroRoot(), AbilityDataPrefabs.AgilityHeroShoot(), AbilityDataPrefabs.AgilityHeroDisengage() },
            //_baseAbilities = new List<AbilityData> { AbilityDataPrefabs.AgilityHeroShoot(), AbilityDataPrefabs.AgilityHeroDisengage() }
            _baseAbilities = new List<AbilityData> { AbilityDataPrefabs.ThrowDagger(), AbilityDataPrefabs.TestRetribution() }
        };
    }

    public static CharacterData Fighter()
    {
        return new CharacterData
        {
            _name = "Fighter",
            _baseHp = 20f,
            _baseMoveSpeed = 1f,
            _baseAbilities = new List<AbilityData> { AbilityDataPrefabs.TankSword() }
        };
    }
    public static CharacterData Healer()
    {
        return new CharacterData
        {
            _name = "Healer",
            _baseHp = 4f,
            _baseMoveSpeed = 1f,
            _baseAbilities = new List<AbilityData> { AbilityDataPrefabs.HealerHeal() }
        };
    }
    

}