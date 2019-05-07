using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviourTree : BehaviourTree
{


    public PatrolEnemyBehaviourTree(CharacterManager characterManager, State defaultState)
    {
        base.Setup(defaultState, characterManager);
    }

    public override void CalculateAndSetNextState(bool forceExitState)
    {
        if (forceExitState)
            ForceExitState(character);

        if (character.brain is ChasingBrain)
        {
            if (character.brain.action)
            {
                character.state.SetState(new AttackState(character, character.characterProperties.attackLoadingTime));
            }
            //TODO: Running if is chasing
            /*else if (character.state is WalkingState)
            {
                character.SetState(new RunningState(character));
            }*/

        }
    }


}
