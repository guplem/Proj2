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

    public override void CalculateAndSetNextState(bool forceExitState)
    {

        if (forceExitState)
            ForceExitState(character);



        // Walking state transitions
        if (character.state is WalkingState)
        {
            // Trigger to enter jumping
            if (character.brain.jumping)
            {
                character.SetState( new JumpingState(character) );
                return;
            }

            // Trigger to enter crouched
            else if (character.brain.crouch)
            {
                character.SetState(new CrouchedState(character) );
                return;
            }

            // Force exit state
            else if (! Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            {
                CalculateAndSetNextState(true);
                return;
            }
        }



        // CrouchedState state transitions
        else if (character.state is CrouchedState)
        {
            // Force exit state
            if (!character.brain.crouch)
            {
                CalculateAndSetNextState(true);
                return;
            }

            // Force exit state
            else if (!Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            {
                CalculateAndSetNextState(true);
                return;
            }
        }



        // OnAirState state transitions
        else if (character.state is OnAirState)
        {
            // Force exit state
            if (Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            {
                CalculateAndSetNextState(true);
                return;
            }
        }




        // Default state transitions (if there is no state)
        if (character.state == null)
        {
            if (Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            {
                character.SetState(new WalkingState(character));
                return;
            }
            else
            {
                character.SetState(new OnAirState(character));
                return;
            }
        }


    }
}