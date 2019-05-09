using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : State
{
    Interactable interactable;

    public PushPullState(CharacterManager characterManager, Interactable interactable)
    {
        this.character = characterManager;
        this.interactable = interactable;
    }

    public override void StartState()
    {
        character.visualsAnimator.SetTrigger("PushPull");
        interactable.StartInteract(character);
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, 0) * 0.5f, character.characterProperties.maxWalkVelocity);
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void OnExit()
    {
        interactable.EndInteract(character);
    }
}
