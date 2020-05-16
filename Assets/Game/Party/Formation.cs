using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public ScreenManager _screenManager;
    public Party _party;
    public eFormation _state;
    protected Dictionary<CharacterData, Character> _characters;

    public void SetFormation(ScreenManager screenManager, Party party)
    {
        _state = eFormation.Engage;
        _screenManager = screenManager;
        _party = party;
    }

    public virtual void InstantiateFormation(ScreenManager manager, eFormation startingState = eFormation.Assemble)
    {
        _characters = new Dictionary<CharacterData, Character>();
        foreach (CharacterData data in _party._formation._formation.Keys)
            _characters[data] = manager.InstantiateCharacter(this, data, (_party._formation._formation[data] + transform.position));
        _state = startingState;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
