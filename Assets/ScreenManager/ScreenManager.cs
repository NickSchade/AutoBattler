using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScreenManager : MonoBehaviour
{
    public ScreenCinematics _cinematics;
    public GameManager _gameManager;
    public List<Character> _characters;
    public PlayerFormation _playerFormation;
    public ScreenTimer _timer;

    public GamePlane _plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<eControl> GetPlayerActions(Player player)
    {
        List<eControl> directionControls = new List<eControl> { eControl.Up, eControl.Down, eControl.Left, eControl.Right };
        List<eControl> actions = new List<eControl>();

        foreach (eControl control in player._controls._controls.Keys)
        {
            KeyCode kc = player._controls._controls[control];
            if (directionControls.Contains(control))
            {
                if (Input.GetKey(kc))
                    actions.Add(control);
            }
            else
            {
                if (Input.GetKeyUp(kc))
                    actions.Add(control);
            }
        }
        return actions;
    }

    public Character InstantiateCharacter(Formation formation, CharacterData data, Vector3 location)
    {
        Character character = Instantiate(_gameManager._prefabs._character, location, Quaternion.identity, this.transform);
        character.SetCharacter(_gameManager, this, formation, data);
        _characters.Add(character);
        return character;
    }
    public PlayerFormation InstantiateFormation(eFormation startingState)
    {
        PlayerFormation playerFormation = Instantiate(_gameManager._prefabs._playerFormation, _plane.GetPartyStartPosition(), Quaternion.identity, this.transform);
        playerFormation.SetFormation(this, _gameManager._party);

        playerFormation.InstantiateFormation(this, startingState);
        return playerFormation;
    }

    public List<int> GetTeamNumbers()
    {
        return _characters.Select(x => x._data._owner._teamNumber).Distinct().ToList();
    }
    public Vector3 GetTeamsCentroid(List<int> teams)
    {
        List<Vector3> teamCentroids = teams.Select(team => GetTeamCentroid(team)).ToList();
        return new Vector3(teamCentroids.Select(x => x.x).Average(), teamCentroids.Select(x => x.y).Average(), teamCentroids.Select(x => x.z).Average());
    }
    public Vector3 GetTeamCentroid(int teamNumber)
    {
        List<Character> teamCharacters = _characters.Where(x => x._data._owner._teamNumber == teamNumber).ToList();
        return GetCharactersCentroid(teamCharacters);
    }
    public Vector3 GetCharactersCentroid()
    {
        return GetCharactersCentroid(_characters);
    }
    public Vector3 GetCharactersCentroid(List<Character> characters)
    {
        List<Vector3> l = characters.Select(x => x.transform.position).ToList();
        Vector3 v = new Vector3(l.Select(x => x.x).Average(), l.Select(x => x.y).Average(), l.Select(x => x.z).Average());
        return v;
    }
}
