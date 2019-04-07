using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTree
{
    void SetNextState(bool forceExitState); //Changes the state if it is necessay
    IState defaultState { get; set; }
}
