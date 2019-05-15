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
        Debug.Log(this.interactable.ToString());
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
        interactableRb2d.AddForce
            (new Vector2(character.brain.direction.x * character.characterProperties.acceleration.x, 0), ForceMode2D.Impulse);

    }

    public override void Tick(float deltaTime)
    {

    }

    public override void OnExit()
    {
        interactable.EndInteract(character);
    }
}
