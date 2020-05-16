using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GamePlane : MonoBehaviour
{
    public Transform _spawnZone;
    public Transform _gameZone;
    public NavMeshSurface _surface;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Vector3> GetDefaultStartPositions()
    {
        //List<Vector3> DefaultStartSpots = new List<Vector3> { new Vector3(-1f, 1f, -3f), new Vector3(-1f, 1f, -1f), new Vector3(-1f, 1f, 1f), new Vector3(-1f, 1f, 3f), new Vector3(1f, 1f, -3f), new Vector3(1f, 1f, -1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 3f), };

        List<Vector3> spots = new List<Vector3>();
        
        spots.Add(new Vector3(-4f, 1f, 8f));
        spots.Add(new Vector3(0f, 1f, 8f));
        spots.Add(new Vector3(4f, 1f, 8f));

        spots.Add(new Vector3(-4f, 1f, 4f));
        spots.Add(new Vector3(0f, 1f, 4f));
        spots.Add(new Vector3(4f, 1f, 4f));

        spots.Add(new Vector3(-4f, 1f, 0f));
        spots.Add(new Vector3(0f, 1f, 0f));
        spots.Add(new Vector3(4f, 1f, 0f));

        spots.Add(new Vector3(-4f, 1f, -4f));
        spots.Add(new Vector3(0f, 1f, -4f));
        spots.Add(new Vector3(4f, 1f, -4f));

        spots.Add(new Vector3(-4f, 1f, -8f));
        spots.Add(new Vector3(0f, 1f, -8f));
        spots.Add(new Vector3(4f, 1f, -8f));

        return spots;
    }
    public Vector3 GetPartyStartPosition()
    {
        return _spawnZone.transform.position;
    }
    public abstract Vector3 GetEnemyStartPosition(int encounterNumber);
    
}
