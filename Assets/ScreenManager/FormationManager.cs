using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FormationManager : ScreenManager
{
    Dictionary<Player, Character> _selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {
        //_cinematics._on = false;
    }
    public void SetFormationManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _plane._surface.BuildNavMesh();
        _playerFormation = InstantiateFormation(eFormation.None);

        _selectedCharacter = new Dictionary<Player, Character>();
        _selectedCharacter[_gameManager._party._players[0]] = _characters[0];
        _selectedCharacter[_gameManager._party._players[1]] = _characters[1];
    }

    // Update is called once per frame
    void Update()
    {
        List<eControl> directionControls = new List<eControl> { eControl.Up, eControl.Down, eControl.Left, eControl.Right };
        foreach (Player player in _gameManager._party._players)
        {
            List<eControl> playerActions = GetPlayerActions(player);

            // SET MOVE
            Vector3 v = _selectedCharacter[player].transform.position;
            Vector3 pos = new Vector3(v.x, v.y, v.z);
            if (playerActions.Contains(eControl.Up))
                pos += Vector3.forward;
            if (playerActions.Contains(eControl.Down))
                pos += Vector3.back;
            if (playerActions.Contains(eControl.Left))
                pos += Vector3.left;
            if (playerActions.Contains(eControl.Right))
                pos += Vector3.right;
            if (pos.x == v.x && pos.y == v.y && pos.z == v.z)
                _selectedCharacter[player].SetMoveStop();
            else
                _selectedCharacter[player].SetMoveTarget(pos);

            // CYCLE
            if (playerActions.Contains(eControl.A))
                SwitchSelected(player);

            // CONFIRM
            if (playerActions.Contains(eControl.B))
                _gameManager._party._formation = GetFormation();
        }
    }

    void SwitchSelected(Player p)
    {
        Debug.Log("Cycling for " + p._name);
    }

    FormationData GetFormation()
    {
        Debug.Log("Getting Formation");
        Dictionary<CharacterData, Vector3> f = new Dictionary<CharacterData, Vector3>();

        foreach (Character character in _characters)
        {
            Vector3 v = character.transform.position;
            if (v.x > -12f)
                f[character._data] = new Vector3(v.x, 1f, v.z);
        }
        FormationData fd = new FormationData(f);
        return fd;
    }
}
