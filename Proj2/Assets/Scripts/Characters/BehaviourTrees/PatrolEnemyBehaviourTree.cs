using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviourTree : BehaviourTree
{
    public EnemyManager character;

    public PatrolEnemyBehaviourTree(IState defaultState, EnemyManager characterManager)
    {
        base.Setup(defaultState);
        this.character = characterManager;
    }

    public override void SetNextState(bool forceExitState)
    {
        if (forceExitState)
            ForceExitState(character);


        //TODO: Call "character.SetState( new ISTATE(...) );" with the state that should be being used.

    }


}
