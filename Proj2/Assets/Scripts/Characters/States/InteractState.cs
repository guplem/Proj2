using System.Collections;
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

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Interact");

        Debug.Log("Begining delay - StartState");
        yield return new WaitForSeconds(actionDelay);
        Debug.Log("Ending delay - StartState");

        interactable.StartInteract(character);
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