using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TripleSliderBlock : MonoBehaviour
{
    public Slider _sliderPrepare;
    public Slider _sliderAct;
    public Slider _sliderLag;

    Action _action;

    public void SetSlider(Action ability)
    {
        _action = ability;
        float totalTime = _action._actionData._duration._prepTime + _action._actionData._duration._activeTime + _action._actionData._duration._lagTime;
        _sliderPrepare.transform.localScale = new Vector3(_action._actionData._duration._prepTime / totalTime, 1f);
        _sliderAct.transform.localScale = new Vector3(_action._actionData._duration._activeTime / totalTime, 1f);
        _sliderLag.transform.localScale = new Vector3(_action._actionData._duration._lagTime / totalTime, 1f);
    }
    
    // Update is called once per frame
    void Update()
    {
        float progress = (_action._character._screenManager._timer.time - _action._stateStartTime) / (_action._nextStateTime - _action._stateStartTime);
        if (_action._state == eActionState.Prepare)
        {
            _sliderPrepare.value = progress;
        }
        else if (_action._state == eActionState.Act)
        {
            _sliderPrepare.value = 1f;
            _sliderAct.value = progress;
        }
        else if (_action._state == eActionState.Lag)
        {
            _sliderAct.value = 1f;
            _sliderLag.value = progress;
        }
    }
}
