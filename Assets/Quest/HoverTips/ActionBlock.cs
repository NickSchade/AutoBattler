using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBlock : MonoBehaviour
{
    public TextBlock _name;
    public TripleSliderBlock _slider;

    public void SetAction(Action action)
    {
        _name.SetText(action._abilityData._name);
        _slider.SetSlider(action);
    }
    
}
