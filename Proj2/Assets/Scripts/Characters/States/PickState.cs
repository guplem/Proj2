using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickState : State
{
    Interactable interactable;
    float actionDelay;

    public PickState(CharacterManager character, Interactable interactable, float actionDelay)
    {
        this.character = character;
        this.interactable = interactable;
        this.actionDelay = actionDelay;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Pick");

        yield return new WaitForSeconds(actionDelay);

        interactable.StartInteract(character);
        character.behaviourTree.CalculateAndSetNextState(true);

        yield return "success";
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
