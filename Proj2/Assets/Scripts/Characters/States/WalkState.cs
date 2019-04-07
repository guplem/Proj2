using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IState
{
    public CharacterManager characterManager { get; set; }

    public WalkingState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void Tick(float deltaTime)
    {
        
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x, characterManager.characterProperties.acceleration.y) );
    }

}