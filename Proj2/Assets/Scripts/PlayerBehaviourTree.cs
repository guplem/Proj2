using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourTree : IBehaviourTree
{
    public CharacterManager character { get; set; }

    public PlayerBehaviourTree(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public IState GetNextState(bool forceExitState)
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
                return new JumpingState();
            }
            //ToDo - Run?
        }
        if (!(character.state is JumpingState))
        {

            if (Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.groundLayer))
            {
                return new GroundedState();
            }
            else
            {
                return new OnAirState();
            }
        }

        return null;
    }
}
