using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractState : State
{
    Interactable interactable;
    float actionDelay;
    float exitDelayAfterAction;

    public InteractState(CharacterManager character, Interactable interactable, float actionDelay, float exitDelayAfterAction)
    {
        this.character = character;
        this.interactable = interactable;
        this.actionDelay = actionDelay;
        this.exitDelayAfterAction = exitDelayAfterAction;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Interact");

        yield return new WaitForSeconds(actionDelay);

        interactable.StartInteract(character);

        yield return new WaitForSeconds(exitDelayAfterAction);

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