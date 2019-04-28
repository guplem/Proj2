using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviourTree : BehaviourTree
{


    public PatrolEnemyBehaviourTree(CharacterManager characterManager, IState defaultState)
    {
        base.Setup(defaultState, characterManager);
    }

    public override void CalculateAndSetNextState(bool forceExitState)
    {
        if (forceExitState)
            ForceExitState(character);


        //TODO: Call "character.SetState( new ISTATE(...) );" with the state that should be being used.

    }


}
