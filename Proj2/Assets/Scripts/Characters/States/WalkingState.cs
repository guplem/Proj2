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
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), character.characterProperties.acceleration, character.characterProperties.internalVelocity);
    }

    public override void OnExit()
    {
        
    }
}