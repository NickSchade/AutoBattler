using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenTimerUi : MonoBehaviour
{
    public TMP_Text _currentTime;
    public TMP_Text _currentScale;
    public ScreenTimer _timer;

    public void Update()
    {
        _currentTime.text = "TIME : " + _timer.time;
        _currentScale.text = "SCALE : " + _timer.timeScale;
    }
}
