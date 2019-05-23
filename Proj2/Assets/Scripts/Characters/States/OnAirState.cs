using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : State
{
    float defaultGravityScale;
    public OnAirState(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Falling");
        defaultGravityScale = character.rb2d.gravityScale;
        character.rb2d.gravityScale = character.characterProperties.OnAirGravityScale;
        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        character.brain.CheckAndFlip();
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x * 0.5f, character.characterProperties.acceleration.y * 0.5f), character.characterProperties.maxOnAirVelocity);
    }

    public override void OnExit()
    {
        character.rb2d.gravityScale = this.defaultGravityScale;
    }
}