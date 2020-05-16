using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IQuestEventHandler
{
    public abstract void HandleEvent(GameEvent ge);
    public abstract void HandleEventAbility(QuestEventAbility gea);
    public abstract void HandleEventEffect(QuestEventEffect gea);
    public abstract void ReceiveEvent(GameEvent ge);

    protected abstract void TakeTick();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TakeTick();
    }
}
