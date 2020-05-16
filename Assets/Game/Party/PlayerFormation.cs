using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFormation : Formation
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void InstantiateFormation(ScreenManager manager, eFormation startingState)
    {
        _characters = new Dictionary<CharacterData, Character>();
        List<Vector3> DefaultStartSpots = manager._plane.GetDefaultStartPositions();
        int index = 0;
        foreach (CharacterData data in _party._formation._formation.Keys)
        {
            Character character = manager.InstantiateCharacter(this, data, (DefaultStartSpots[index] + transform.position));
            _characters[data] = character;
            index++;
        }
        transform.position = new Vector3(transform.position.x + 15f, transform.position.y, transform.position.z);
        _state = startingState;
    }
    // Update is called once per frame
    void Update()
    {
        if (_state == eFormation.Assemble)
        {
            bool assembled = true;
            foreach (CharacterData cd in _characters.Keys)
            {
                if (Vector3.Distance(_characters[cd].transform.position, _party._formation._formation[cd] + transform.position) > 0.5f)
                {
                    _characters[cd].SetMoveTarget(_party._formation._formation[cd] + transform.position);
                    assembled = false;
                }
                else
                {
                    _characters[cd].SetMoveStop();
                }
            }
            if (assembled)
            {
                _state = eFormation.Move;
            }
        }
        if (_state == eFormation.Move)
        {
            _state = eFormation.None;
            _screenManager._gameManager._questManager.InstantiateEncounter(0);
            _state = eFormation.Engage;
        }
        if (_state == eFormation.Engage)
        {

        }
    }
}
