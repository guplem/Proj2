using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : State
{
    Interactable interactable;
    Rigidbody2D interactableRb2d;

    public PushPullState(CharacterManager characterManager, Interactable interactable)
    {
        this.character = characterManager;
        this.interactable = interactable;
        this.interactableRb2d = interactable.GetComponent<Rigidbody2D>();
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("PushPull");
        interactable.StartInteract(character);
        yield return "success";
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, 0) * 0.5f, character.characterProperties.maxWalkVelocity);
        //Debug.Log(character.brain.direction.x * character.characterProperties.acceleration.x);
        interactableRb2d.velocity = new Vector2(Mathf.Min(character.brain.direction.x * character.characterProperties.acceleration.x, character.rb2d.velocity.x), 0);

    }

    public override void Tick(float deltaTime)
    {

    }

    public override void OnExit()
    {
        interactable.EndInteract(character);
    }
}
