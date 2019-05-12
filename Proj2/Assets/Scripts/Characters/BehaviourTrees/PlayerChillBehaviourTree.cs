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

        //State.SetState(defaultState, character);

    }

    /////////////////////////////////////////////////////

    private bool EnterIdle()
    {
        if (!character.isTouchingGround())
            return false;

        State.SetState(new IdleState(character), character);
        return true;
    }

    private bool EnterThrow()
    {
        if (!character.brain.action)
            return false;

        if (!((PlayerManager)character).inventory.HasStoredItem())
            return false;

        if (!character.isTouchingGround())
            return false;

        State.SetState(new ThrowState(character), character);
        return true;

    }

    private bool EnterPushPull()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.GetAvaliableInterectable(Activable.ActivationType.Movable);
        if (interactable == null)
            return false;

        State.SetState(new PushPullState(character, interactable), character);
        return true;
    }

    private bool EnterInteract()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.GetAvaliableInterectable(Activable.ActivationType.Other);
        if (interactable == null)
            return false;

        State.SetState(new InteractState(character, interactable, 2f), character);
        return true;
    }

    private bool EnterPick()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsCollider.GetAvaliableInterectable(Activable.ActivationType.Pickable);
        if (interactable == null)
            return false;

        State.SetState(new PickState(character, interactable, 2f), character);
        return true;
    }

    private bool EnterCrouched()
    {
        if (!character.isTouchingGround())
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

        if ( (character.rb2d.velocity.y > 0.1f) || (character.rb2d.velocity.y < -0.1f) )
            return false;

        State.SetState(new WalkingState(character), character);
        return true;
    }

    private bool EnterOnAir()
    {
        if (character.isTouchingGround())
            return false;

        if (!(character.rb2d.velocity.y <= 0.1f))
            return false;

        State.SetState(new OnAirState(character), character);
        return true;
    }

    private bool EnterJump()
    {
        if (!character.brain.jumping)
            return false;

        if (!character.isTouchingGround())
            return false;
        /*if (character.GetComponent<Rigidbody2D>().velocity.y <= 0.1f)
            return false;*/

        State.SetState(new JumpingState(character), character);
        return true;
    }
}