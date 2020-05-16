using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCapsule : Character
{

    Material _material;
    Color _startingColor;

    void PositionGameObject(GameObject go, float x = 0f, float y = 0f, float z = 0f)
    {
        go.transform.localPosition = new Vector3(x, y, z);
    }
    void RotateGameObject(GameObject go, float x = 0f, float y = 0f, float z = 0f)
    {
        go.transform.localRotation = Quaternion.Euler(new Vector3(x, y, z));
    }


    public override void PoseNeutral()
    {
        PositionGameObject(_handRight, 0.75f, -0.1f);
        RotateGameObject(_handRight, 90f);

        PositionGameObject(_handLeft, -0.75f, -0.1f);
        RotateGameObject(_handLeft, 90f);
    }
    public override void PoseMeleeStart()
    {
        PositionGameObject(_handRight, 0.75f, 0.5f, -0.15f);
        RotateGameObject(_handRight, -45f);

        PositionGameObject(_handLeft, -0.5f, 0.2f, 0.5f);
        RotateGameObject(_handLeft, 90f);
    }

    public override void PoseMeleeStrike()
    {
        PositionGameObject(_handRight, 0.75f, -0.1f, 0.5f);
        RotateGameObject(_handRight, 90f);

        PositionGameObject(_handLeft, -0.5f, 0.2f, -0.5f);
        RotateGameObject(_handLeft, 90f, -45f);
    }

    public override void PoseMeleeFinish()
    {
        PositionGameObject(_handRight, 0.75f, -0.25f, 0.35f);
        RotateGameObject(_handRight, 115f);

        PositionGameObject(_handLeft, -0.4f, 0f, -0.4f);
        RotateGameObject(_handLeft, 90f, -60f);
    }

    public override void PoseWalkLeft()
    {
        PositionGameObject(_handRight, 0.75f, 0f, -0.3f);
        RotateGameObject(_handRight, 135f);

        PositionGameObject(_handLeft, -0.75f, 0f, 0.3f);
        RotateGameObject(_handLeft, 45f);
    }
    public override void PoseWalkRight()
    {
        PositionGameObject(_handRight, 0.75f, 0f, 0.3f);
        RotateGameObject(_handRight, 45f);

        PositionGameObject(_handLeft, -0.75f, 0f, -0.3f);
        RotateGameObject(_handLeft, 135f);
    }


    public override void HandleEventAbility(QuestEventAbility gea)
    {
        if (gea._state == eActionState.Act && gea._targets.Contains(this))
            gea._ability.Trigger(this);
    }

    public override void HandleEventEffect(QuestEventEffect gea)
    {
        if (gea._target == this)
            AddEffect(new Effect(gea._actor, gea._effect));
    }
    protected override IEnumerator TakeDamage()
    {
        _material.color = Color.red;
        yield return null;
        _material.color = _startingColor;
        yield return null;
    }

    protected override IEnumerator HealDamage()
    {
        _material.color = Color.green;
        yield return null;
        _material.color = _startingColor;
        yield return null;
    }
    protected override void AbstractInitialize()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.color = _data._owner._teamNumber == 0 ? Color.cyan: Color.grey;
        _startingColor = new Color(_material.color.r, _material.color.g, _material.color.b);
    }

    public override void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }
}