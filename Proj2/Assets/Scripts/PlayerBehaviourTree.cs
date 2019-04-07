using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourTree : IBehaviourTree
{
    public bool CheckForNextState(CharacterManager character, bool forceExitState)
    {
        //base.CheckTransition(forceExitState);
        if (forceExitState)
        {
            character.ChangeState(null, character);
        }
        if (character.state is GroundedState)
        {
            if (character.inputController.jumping)
            {
                //Check transitions
                return character.ChangeState(new JumpingState(), character);
            }
            //ToDo - Run?
        }
        if (!(character.state is JumpingState))
        {

            if (Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.groundLayer))
            {
                return character.ChangeState(new GroundedState(), character);
            }
            else
            {
                return character.ChangeState(new OnAirState(), character);
            }
        }
        return false;
    }
}
