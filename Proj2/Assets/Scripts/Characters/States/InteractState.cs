﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    Interactable interactable;
    float actionDelay;

    public InteractState(CharacterManager character, Interactable interactable, float actionDelay)
    {
        this.character = character;
        this.interactable = interactable;
        this.actionDelay = actionDelay;
    }

    public override void StartState()
    {
        character.visualsAnimator.SetTrigger("Interact");
        Wait(actionDelay);
        interactable.StartInteract(character);
        character.behaviourTree.CalculateAndSetNextState(true);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {

    }

    public override void OnExit()
    {
        interactable.EndInteract(character);
    }
}
