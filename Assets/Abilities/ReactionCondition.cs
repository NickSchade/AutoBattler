using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionCondition
{
}

public class AbilityReactionCondition : ReactionCondition
{
    public List<eActionState> _states;
    public List<eEffect> _effects;
    public List<eTarget> _reactorToActor;
    public List<eTarget> _reactorToTarget;
    public eAbilityReactionTarget _reactionTarget;

    public AbilityReactionCondition(List<eActionState> states, List<eEffect> effects, List<eTarget> reactorToActor, List<eTarget> reactorToTarget, eAbilityReactionTarget reactionTarget)
    {
        _states = states;
        _effects = effects;
        _reactorToActor = reactorToActor;
        _reactorToTarget = reactorToTarget;
        _reactionTarget = reactionTarget;
    }
}