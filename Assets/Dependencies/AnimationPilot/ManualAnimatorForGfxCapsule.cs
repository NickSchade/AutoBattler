using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAnimatorForGfxCapsule : MonoBehaviour
{
    public CharacterGfx _character;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
            _character.PoseNeutral();
        if (Input.GetKeyUp(KeyCode.W))
            _character.PoseMeleeStart();
        if (Input.GetKeyUp(KeyCode.E))
            _character.PoseMeleeStrike();
        if (Input.GetKeyUp(KeyCode.R))
            _character.PoseMeleeFinish();
        if (Input.GetKeyUp(KeyCode.A))
            _character.PoseWalkLeft();
        if (Input.GetKeyUp(KeyCode.D))
            _character.PoseWalkRight();
    }
}
