using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlane : GamePlane
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override Vector3 GetEnemyStartPosition(int encounterNumber)
    {
        return new Vector3(10f, 0f, 0f);
    }
}
