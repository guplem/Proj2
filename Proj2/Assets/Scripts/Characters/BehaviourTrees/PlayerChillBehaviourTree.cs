using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChillBehaviourTree : BehaviourTree
{
    public PlayerManager character;

    public PlayerChillBehaviourTree(IState defaultState, PlayerManager characterManager)
    {
        base.Setup(defaultState);
        this.character = characterManager;
    }

    public override void SetNextState(bool forceExitState)
    {

        if (forceExitState)
            ForceExitState(character);

        //base.CheckTransition(forceExitState);
        if (character.state is WalkingState)
        {
            if (character.brain.jumping)
            {
                //Check transitions
                character.SetState( new JumpingState(character) );
            }
            //ToDo - Run?
        }
        if (!(character.state is JumpingState))
        {
            // Added the check on the interactablesLayer, allows the player to walk on interactable physics objects.
            if (Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.groundLayer + GameManager.Instance.interactablesLayer))
            {
                character.SetState( new WalkingState(character) );
            }
            else
            {
                if (!(character.state is OnAirState))
                    character.SetState( new OnAirState(character) );
            }
        }


    }
}
