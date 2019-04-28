using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree
{
    protected CharacterManager character;
    public IState defaultState;

    public void Setup(IState defaultState, CharacterManager character)
    {
        this.defaultState = defaultState;
        this.character = character;
    }

    public abstract void CalculateAndSetNextState(bool forceExitState);

    protected void ForceExitState(CharacterManager character)
    {
        character.SetState(null);
    }

}
