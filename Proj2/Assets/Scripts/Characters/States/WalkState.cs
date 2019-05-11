using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : State
{
    public WalkingState(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Walk");
        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, character.characterProperties.acceleration.y), character.characterProperties.maxWalkVelocity);
    }

    public override void OnExit()
    {
        
    }
}