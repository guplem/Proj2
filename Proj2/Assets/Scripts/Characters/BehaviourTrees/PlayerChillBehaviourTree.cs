﻿using System;
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

        if (EnterDead()) return;
        if (EnterRevive()) return;
        if (EnterOnAir()) return;
        if (EnterPushPull()) return;
        if (EnterPick()) return;
        if (EnterInteract()) return;
        if (EnterThrow()) return;
        if (EnterJump()) return;
        if (EnterCrouched()) return;
        if (EnterWalking()) return;
        if (EnterIdle()) return;

    }

    /////////////////////////////////////////////////////

    private bool EnterThrow()
    {

        if (!character.brain.actionDown && !(character.state is ThrowState))
            return false;

        if (!((PlayerManager)character).inventory.HasStoredItem() && !(character.state is ThrowState))
            return false;

        if (!character.IsTouchingGround())
            return false;

        State.SetState(new ThrowState((PlayerManager)character), character);
        return true;

    }

    private bool EnterRevive()
    {
        if (!(character.state is ReviveState))
            return false;

        State.SetState(new ReviveState(), character);
        return true;
    }

    private bool EnterPushPull()
    {
        if (!character.brain.interact)
            return false;

        Interactable interactable = character.interactionsController.GetAvaliableInterectable(Activable.ActivationType.Movable);
        if (interactable == null)
            return false;

        State.SetState(new PushPullState(character, interactable), character);
        return true;
    }

    private bool EnterInteract()
    {
        if (!character.brain.interact && !(character.state is InteractState))
            return false;

        Interactable interactable = character.interactionsController.GetAvaliableInterectable(Activable.ActivationType.Other);
        if (interactable == null)
            return false;

        if (interactable.interactAutomatically)
            return false;

        State.SetState(new InteractState(character, interactable, 0.27f, 0.8f), character);
        return true;
    }

    private bool EnterPick()
    {
        if (!character.brain.interact && !(character.state is PickState))
            return false;

        Interactable interactable = character.interactionsController.GetAvaliableInterectable(Activable.ActivationType.Pickable);
        if (interactable == null)
            return false;

        State.SetState(new PickState(character, interactable, 0.63f), character);
        return true;
    }

}