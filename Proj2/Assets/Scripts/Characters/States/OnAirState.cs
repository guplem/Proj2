using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : State
{

    public OnAirState(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x * 0.5f, character.characterProperties.acceleration.y * 0.5f), character.characterProperties.maxOnAirVelocity);
    }

    public override void OnExit()
    {
        
    }
}