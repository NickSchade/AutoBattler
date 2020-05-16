using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class QuestManager : ScreenManager, IQuestEventHandler
{
    public HoverTipManager _hoverTipManager;
    
    Encounters _encounters;
    List<GameEvent> _events;
    
    // Start is called before the first frame update
    void Start()
    {
        _plane = Instantiate(_gameManager._prefabs._testPlane, transform);
        _plane._surface.BuildNavMesh();
        _characters = new List<Character>();
        _events = new List<GameEvent>();

        InitializeTest();
    }

    private void Update()
    {
        _hoverTipManager.UpdateCharacterStats(_characters);
    }

    
    
    void InitializeTestEncounter()
    {
        _encounters = new Encounters(this);
        _encounters._enemy.AddCharacterToRoster(CharacterData.Fighter());
        FormationData enemyFormation = new FormationData(new Dictionary<CharacterData, Vector3> { { _encounters._enemy._roster[0], new Vector3(0f, 1f, -3f) } } );
        _encounters.AddEncounter(new Party(new List<Player> { _encounters._enemy }, enemyFormation));
    }

    
    void InitializeTest()
    {
        InitializeTestEncounter();

        _playerFormation =  InstantiateFormation(eFormation.Assemble);
    }
    public void InstantiateEncounter(int encounterNumber)
    {
        Formation formation = Instantiate(_gameManager._prefabs._enemyFormation, _plane.GetEnemyStartPosition(encounterNumber), Quaternion.identity, transform);
        formation.SetFormation(this, _encounters._encounters[encounterNumber]);
        formation.InstantiateFormation(this, eFormation.Engage);
    }

    public void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }

    public void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
        _cinematics.ReceiveEvent(ge);
        _hoverTipManager.ReceiveEvent(ge);
        List<Character> characters = _characters.ToList();
        foreach (Character character in characters)
            character.ReceiveEvent(ge);
    }
    
    public void HandleEventAbility(QuestEventAbility gea)
    {
        //
    }

    public void HandleEventEffect(QuestEventEffect gea)
    {
        //
    }
}
