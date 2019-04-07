using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : IBehaviourTree
{
    public IState defaultState { get; set; }

    public void Setup(IState defaultState)
    {
        this.defaultState = defaultState;
    }

    public virtual void SetNextState(bool forceExitState)
    {
        Debug.LogError("Overriding of 'GetNextState()' not implemented in a class that inherits from 'BehaviourTree'.");
    }

    protected void ForceExitState(CharacterManager character)
    {
        character.SetState(null);
    }

}
