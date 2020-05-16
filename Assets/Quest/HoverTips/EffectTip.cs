using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectTip : MonoBehaviour
{
    public TMP_Text _text;

    QuestEventEffect _gee;
    Vector3 _characterToScreenPosition;

    public void SetEffect(QuestEventEffect gee, AbilityEffect effect)
    {
        _text.text = effect._quantity.ToString();
        _text.color = GetColor(effect._type);
        _gee = gee;
        StartCoroutine(FloatAndDisappear());
    }

    void UpdatePosition()
    {
        _characterToScreenPosition = _gee._actor._screenManager._cinematics._camera.WorldToScreenPoint(_gee._target.transform.localPosition);
    }
    

    IEnumerator FloatAndDisappear()
    {
        UpdatePosition();
        _text.transform.position = _characterToScreenPosition;

        float startTime = _gee._actor._screenManager._timer.time;
        float endTime = startTime + 1f;

        while (_gee._actor._screenManager._timer.time < endTime)
        {
            float f = (_gee._actor._screenManager._timer.time - startTime) / (endTime - startTime);
            UpdatePosition();
            _text.transform.position = new Vector3(_characterToScreenPosition.x, _characterToScreenPosition.y + 100 * f);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;        
    }

    Color GetColor(eEffect effect)
    {
        if (effect == eEffect.Damage)
            return Color.red;
        else if (effect == eEffect.Heal)
            return Color.green;
        else if (effect == eEffect.Root)
            return Color.yellow;
        else if (effect == eEffect.Stun)
            return Color.black;
        else
            throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
