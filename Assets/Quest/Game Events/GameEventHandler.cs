using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestEventHandler 
{
    void HandleEvent(GameEvent ge);
    //{
    //    if (ge is QuestEventAbility gea)
    //        HandleEventAbility(gea);
    //    else if (ge is QuestEventEffect ges)
    //        HandleEventEffect(ges);
    //}
    void ReceiveEvent(GameEvent ge);
    void HandleEventAbility(QuestEventAbility gea);
    void HandleEventEffect(QuestEventEffect gea);
}
