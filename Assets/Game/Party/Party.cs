using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Party
{
    public List<Player> _players;
    public FormationData _formation;
    public Party()
    {
        _players = new List<Player>();
    }
    public Party(List<Player> players, FormationData formation)
    {
        _players = players;
        _formation = formation;
    }
}
public class Encounters
{
    public Player _enemy;
    public List<Party> _encounters;
    public Encounters(QuestManager questManager)
    {
        _enemy = new Player();
        _encounters = new List<Party>();
    }
    public void AddEncounter(Party encounter)
    {
        _encounters.Add(encounter);
    }
}

public class FormationData 
{
    public Dictionary<CharacterData, Vector3> _formation;
    public FormationData(Dictionary<CharacterData, Vector3> formation)
    {
        _formation = formation;
    }
}


