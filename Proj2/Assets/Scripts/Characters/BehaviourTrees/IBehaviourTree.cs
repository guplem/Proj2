using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTree
{
    CharacterManager character { get; set; }
    IState GetNextState(bool forceExitState);
}
