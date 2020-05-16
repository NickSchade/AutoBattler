using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlock : MonoBehaviour
{
    public TextBlock _nameBlock;
    public SliderBlock _hpBlock;
    
    public void UpdateStatBlock(Character character)
    {
        _nameBlock.SetText(character._data._name);
        _hpBlock.SetSlider($"{character._currentHp} / {character._baseHp}", character._currentHp, character._baseHp);
    }
    public void UpdateStatBlock(Action ability)
    {
        _nameBlock.SetText(ability._abilityData._name);
        _hpBlock.SetSlider(ability._state.ToString(), ability._character._screenManager._timer.time - ability._stateStartTime, ability._nextStateTime-ability._stateStartTime);
    }
}
