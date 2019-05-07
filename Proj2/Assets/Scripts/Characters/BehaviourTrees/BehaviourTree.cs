using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree
{
    protected CharacterManager character;
    public State defaultState;

    public void Setup(State defaultState, CharacterManager character)
    {
        this.defaultState = defaultState;
        this.character = character;
    }

    public abstract void CalculateAndSetNextState(bool forceExitState);

    protected void ForceExitState(CharacterManager character)
    {
        character.state.SetState(null);
    }

}
