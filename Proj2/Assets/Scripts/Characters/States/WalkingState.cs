using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : State
{

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Walk");
        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        character.brain.CheckAndFlip();
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        Vector2 dir = Vector2.zero;
        if (character.brain.direction != Vector2.zero)
        {
            dir = character.brain.direction;
        }
        else
        {
             //dir = new Vector2(0, 0);
            dir = new Vector2(Mathf.Lerp(-character.rb2d.velocity.x, 0, 0.8f), 0);
        }

        character.movementController.MoveTowards(new Vector2(dir.x, 0), character.characterProperties.acceleration, character.characterProperties.internalVelocity);
    }

    public override void OnExit()
    {
        
    }
}