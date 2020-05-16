using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderBlock : MonoBehaviour
{
    public Slider _slider;
    public TMP_Text _text;

    public void SetSlider(string text, float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
        _text.text = text;
    }
}
