using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTree
{
    bool CheckForNextState(CharacterManager characterManager, bool forceExitState);
}
