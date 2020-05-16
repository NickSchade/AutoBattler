using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NmtGameManager : MonoBehaviour
{
    public NavMeshSurface _surface;
    // Start is called before the first frame update
    void Start()
    {
        _surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
