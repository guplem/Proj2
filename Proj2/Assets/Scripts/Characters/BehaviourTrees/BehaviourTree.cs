using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree
{
    public IState defaultState;

    public void Setup(IState defaultState)
    {
        this.defaultState = defaultState;
    }

    public abstract void SetNextState(bool forceExitState);

    protected void ForceExitState(CharacterManager character)
    {
        character.SetState(null);
    }

}
