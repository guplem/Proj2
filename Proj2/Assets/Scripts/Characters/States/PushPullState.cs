using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : State
{
    Interactable interactable;
    Rigidbody2D interactableRb2d;
    private bool isPushing;
    private bool isPulling;

    public PushPullState(CharacterManager characterManager, Interactable interactable)
    {
        this.character = characterManager;
        this.interactable = interactable;
        this.interactableRb2d = interactable.GetComponent<Rigidbody2D>();
    }

    protected override IEnumerator StartState()
    {
        interactable.StartInteract(character);
        character.rb2d.velocity = Vector3.zero;
        yield return "success";
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, 0), character.characterProperties.maxWalkVelocity);
        interactableRb2d.velocity = new Vector2(character.rb2d.velocity.x, 0);

    }

    public override void Tick(float deltaTime)
    {
        if (interactable.transform.position.x < character.transform.position.x)
        {
            if (character.brain.direction.x <= 0)
            {
                // Push
                SetAnim(true);
            }
            else
            {
                // Pull
                SetAnim(false);
            }

        }
        else
        {
            if (character.brain.direction.x >= 0)
            {
                // Push
                SetAnim(true);
            }
            else
            {
                // Pull
                SetAnim(false);
            }

        }
    }

    public void SetAnim(bool pushAnim)
    {
        if (pushAnim)
        {
            if (!isPushing)
            {
                isPushing = true;
                character.visualsAnimator.SetTrigger("Push");
            }
            isPulling = false;
        }
        else
        {
            if (!isPulling)
            {
                isPulling = true;
                character.visualsAnimator.SetTrigger("Pull");
            }
            isPushing = false;
        }
    }

    private bool SetAnimation()
    {
        
        return isPushing;
    }

    public override void OnExit()
    {
        interactable.EndInteract(character);
    }
}
