using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeScaler : MonoBehaviour
{
    public TMP_Text _text;

    // Update is called once per frame
    void Update()
    {
        _text.text = Time.timeScale.ToString();
    }
}
