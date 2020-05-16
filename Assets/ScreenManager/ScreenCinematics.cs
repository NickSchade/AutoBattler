using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenCinematics : MonoBehaviour, IQuestEventHandler
{
    public bool _on;
    public ScreenManager _screenManager;

    public Camera _camera;
    public Light _light;
    public Color _startingLightColor;
    
    Vector3 _centroidPosition;
    Vector3 _targetPosition;

    float _sloMoFraction = 0.5f;
    float _zoomPercent = 1f;


    List<QuestEventAbility> _dynamicEvents;


    // Start is called before the first frame update
    void Start()
    {
        _startingLightColor = new Color(_light.color.r, _light.color.g, _light.color.b);
        _dynamicEvents = new List<QuestEventAbility>();
        if (_on)
            _centroidPosition = _screenManager.GetTeamsCentroid(_screenManager.GetTeamNumbers());
    }

    Vector3 GetCameraNeutral()
    {
        return new Vector3(0f, 20f, -6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_on)
            return;
        
        _centroidPosition = _screenManager.GetTeamsCentroid(_screenManager.GetTeamNumbers());

        HandleCenter();

        HandleMove();
    }

    
    public void ReceiveEvent(GameEvent ge)
    {
        if (!_on)
            return;

        HandleEvent(ge);
    }
    public void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }
    public void HandleEventEffect(QuestEventEffect gea)
    {
        //
    }
    public void HandleEventAbility(QuestEventAbility gea)
    {
        // HANDLE QUEUE
        if (gea._state == eActionState.Prepare || gea._state == eActionState.Act || gea._state == eActionState.Instant)
            _dynamicEvents.Add(gea);
        else if (gea._state == eActionState.Ready || gea._state == eActionState.Lag || gea._state == eActionState.Cancelled)
            foreach (QuestEventAbility ability in _dynamicEvents.Where(x => x._ability == gea._ability).ToList())
                _dynamicEvents.Remove(ability);

        int numberOfInstantActions = _dynamicEvents.Where(x => x._state == eActionState.Instant).ToList().Count;
        if (numberOfInstantActions == 0)
        {
            HandleSloMo();
            HandleZoom();
        }
        else
        {

        }
    }

    void HandleSloMo()
    {

        if (_dynamicEvents.Count > 0)
            _screenManager._timer.timeScale = _sloMoFraction;
        else
            _screenManager._timer.timeScale = 1f;
    }
    void HandleZoom()
    {
        if (_dynamicEvents.Count > 0)
            _zoomPercent = 0.8f;
        else
            _zoomPercent = 1f;
    }
    void HandleCenter()
    {
        if (_dynamicEvents.Count == 0)
            _targetPosition = _centroidPosition;
        else
            _targetPosition = GetActionPosition();
    }
    Vector3 GetActionPosition()
    {
        QuestEventAbility questEvent = _dynamicEvents[_dynamicEvents.Count - 1];
        if (questEvent._state == eActionState.Prepare)
            return questEvent._actor.transform.position;
        else if (questEvent._state == eActionState.Act)
            return questEvent._target;
        else
            return questEvent._target;
    }
    void HandleMove()
    {
        float speed = 0.01f;
        _camera.transform.position = (1f - speed) * _camera.transform.position + speed * GetCameraPosition(_targetPosition);
    }
    Vector3 GetCameraPosition(Vector3 targetPosition)
    {
        return (targetPosition + _zoomPercent * GetCameraNeutral());
    }
}
