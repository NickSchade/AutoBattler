using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterGfx : MonoBehaviour
{
    protected Rigidbody _rigidBody;
    protected NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        PoseNeutral();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void PoseNeutral();

    public abstract void PoseWalkRight();
    public abstract void PoseWalkLeft();


    public abstract void PoseMeleeStart();
    public abstract void PoseMeleeStrike();
    public abstract void PoseMeleeFinish();
}
