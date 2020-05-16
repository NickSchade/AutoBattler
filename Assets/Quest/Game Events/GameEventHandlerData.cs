using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestEventHandlerData
{
    public void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }
    public abstract void ReceiveEvent(GameEvent ge);
    protected abstract void HandleEventAbility(QuestEventAbility gea);
    protected abstract void HandleEventEffect(QuestEventEffect gea);
}

