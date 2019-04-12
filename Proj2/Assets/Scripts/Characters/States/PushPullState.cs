using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : IState
{
    public CharacterManager characterManager { get; set; }

    public PushPullState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x, 0) * 0.5f, characterManager.characterProperties.maxWalkVelocity);
    }

    public void Tick(float deltaTime)
    {

    }

    public void OnExit()
    {
        
    }
}
