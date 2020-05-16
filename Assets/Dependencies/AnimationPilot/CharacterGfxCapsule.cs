using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGfxCapsule : CharacterGfx
{
    public GameObject _handRight;
    public GameObject _handLeft;

    public override void PoseNeutral()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, -0.1f, 0f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.75f, -0.1f, 0f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }
    public override void PoseMeleeStart()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, 0.5f, -0.15f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(-45f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.5f, 0.2f, 0.5f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(90f, 45f, 0f));
    }

    public override void PoseMeleeStrike()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, -0.1f, 0.5f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.5f, 0.2f, -0.5f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(90f, -45f, 0f));
    }

    public override void PoseMeleeFinish()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, -0.25f, 0.35f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(115f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.4f, 0.0f, -0.4f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(90f, -60f, 0f));
    }

    public override void PoseWalkLeft()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, 0f, -0.3f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(135f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.75f, 0.0f, 0.3f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(45, 0f, 0f));
    }
    public override void PoseWalkRight()
    {
        _handRight.transform.localPosition = new Vector3(0.75f, 0f, 0.3f);
        _handRight.transform.localRotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));

        _handLeft.transform.localPosition = new Vector3(-0.75f, 0.0f, -0.3f);
        _handLeft.transform.localRotation = Quaternion.Euler(new Vector3(135f, 0f, 0f));
    }
}
