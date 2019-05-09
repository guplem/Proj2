using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChillBehaviourTree : BehaviourTree
{
    public PlayerChillBehaviourTree(State defaultState, PlayerManager characterManager)
    {
        base.Setup(defaultState, characterManager);
    }

    public override void CalculateAndSetNextState(bool forceExitState)
    {

        if (forceExitState)
            ForceExitState(character);

        //if (EnterAttack()) return;
        if (EnterPushPull()) return;
        if (EnterJump()) return;
        if (EnterOnAir()) return;
        if (EnterWalking()) return;
        if (EnterCrouched()) return;
        if (EnterPick()) return;
        if (EnterInteract()) return;
        if (EnterThrow()) return;
        if (EnterIdle()) return;

        State.SetState(defaultState, character);

        /*
                // Walking state transitions
                if (character.state is WalkingState)
                {
                    // Trigger to enter jumping 
                    if (character.brain.jumping && character.GetComponent<Rigidbody2D>().velocity.y <= 0.1f)
                    {
                        State.SetState( new JumpingState(character) , character);
                        return;
                    }

                    // Trigger to enter crouched
                    else if (character.brain.crouch)
                    {
                        State.SetState( new CrouchedState(character) , character);
                        return;
                    }

                    // Trigger to enter throw mode
                    else if (character.brain.action && ((PlayerManager)character).inventory.HasStoredItem())
                    {
                        State.SetState( new ThrowState(character), character);
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
                        State.SetState(new WalkingState(character), character);
                        return;
                    }
                    else
                    {
                        State.SetState(new OnAirState(character), character);
                        return;
                    }
                }

            */
    }

    /////////////////////////////////////////////////////

    private bool isTouchingGround()
    {
        return Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers);
    }

    /////////////////////////////////////////////////////





    private bool EnterIdle()
    {
        if (!isTouchingGround())
            return false;

        State.SetState(new IdleState(character), character);
        return true;
    }

    private bool EnterThrow()
    {
        if (!(character.brain.action))
            return false;

        if (((PlayerManager)character).inventory.HasStoredItem())
            return false;

        if (!isTouchingGround())
            return false;

        State.SetState(new ThrowState(character), character);
        return true;

    }

    private bool EnterPushPull()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.CanInteractWith(Activable.ActivationType.Movable);
        if (interactable != null)
            return false;

        State.SetState(new PushPullState(character, interactable), character);
        return true;
    }

    private bool EnterInteract()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.CanInteractWith(Activable.ActivationType.Activable);
        if (interactable != null)
            return false;

        State.SetState(new InteractState(character, interactable, 2f), character);
        return true;
    }

    private bool EnterPick()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.CanInteractWith(Activable.ActivationType.Pickable);
        if (interactable != null)
            return false;

        State.SetState(new PickState(character, interactable, 2f), character);
        return true;
    }

    private bool EnterCrouched()
    {
        if (!isTouchingGround())
            return false;

        if (!character.brain.crouch)
            return false;

        State.SetState(new CrouchedState(character), character);
        return true;
    }

    private bool EnterWalking()
    {
        if (!Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            return false;

        if (character.brain.direction.x == 0)
            return false;

        State.SetState(new WalkingState(character), character);
        return true;
    }

    private bool EnterOnAir()
    {
        if (isTouchingGround())
            return false;

        if (! (character.GetComponent<Rigidbody2D>().velocity.y <= 0.1f))
            return false;

        State.SetState(new OnAirState(character), character);
        return true;
    }

    private bool EnterJump()
    {
        if (!character.brain.jumping)
            return false;

        if (!isTouchingGround())
            return false;
        /*if (character.GetComponent<Rigidbody2D>().velocity.y <= 0.1f)
            return false;*/

        State.SetState(new JumpingState(character), character);
        return true;
    }
}