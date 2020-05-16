using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestManager _qmPrefab;
    public FormationManager _fmPrefab;

    public GamePrefabs _prefabs;
    
    public QuestManager _questManager;
    public FormationManager _formationManager;
    public Party _party;

    // Start is called before the first frame update
    void Start()
    {
        _party = InitializeTestParty();
    }
    void InstantiateFormationManager()
    {
        _formationManager = Instantiate(_fmPrefab, this.transform);
        _formationManager.SetFormationManager(this);
    }
    void InstantiateQuestManager()
    {
        _questManager = Instantiate(_qmPrefab, this.transform);
        _questManager._gameManager = this;
    }
    Party InitializeTestParty()
    {
        Party party = new Party();
        party._players.Add(Player.P1());
        //party._players.Add(Player.P2());
        
        party._players[0].AddCharacterToRoster(CharacterData.AgilityHero());
        //party._players[1].AddCharacterToRoster(CharacterData.Healer());

        Dictionary<CharacterData, Vector3> formation = new Dictionary<CharacterData, Vector3> {
            { party._players[0]._roster[0], new Vector3(0f, 1f, 3f) },
            //{ party._players[1]._roster[0], new Vector3(-2f, 1f, -1f) }
        };
        
        party._formation = new FormationData(formation);
        return party;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            InstantiateQuestManager();
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            InstantiateFormationManager();
    }
}
