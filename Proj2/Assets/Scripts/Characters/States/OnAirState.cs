using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : IState
{
    public CharacterManager characterManager { get; set; }

    public OnAirState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void Tick(float deltaTime)
    {

    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y * 0.5f), characterManager.characterProperties.maxOnAirVelocity);
    }

}