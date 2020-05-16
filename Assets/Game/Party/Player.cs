using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player
{
    public string _name;
    public int _teamNumber;
    public List<CharacterData> _roster;
    public ControlSet _controls;

    public Player(ControlSet controls = null, string name = "")
    {
        _controls = controls;
        InitializeName(name);
        _roster = new List<CharacterData>();
    }
    void InitializeName(string name)
    {
        if (name == "" || name == "Enemy")
        {
            _name = "Enemy";
            _teamNumber = 1;
        }
        else
        {
            _name = name;
            _teamNumber = 0;
        }
    }
    public void AddCharacterToRoster(CharacterData data)
    {
        data._owner = this;
        _roster.Add(data);
    }

    public static Player P1()
    {
        return new Player(ControlSet.P1(), "P1");
    }
    public static Player P2()
    {
        return new Player(ControlSet.P2(), "P2");
    }
}

