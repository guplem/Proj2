using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : IState
{
    public CharacterManager character { get; set; }

    public PushPullState(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, 0) * 0.5f, character.characterProperties.maxWalkVelocity);
    }

    public void Tick(float deltaTime)
    {

    }

    public void OnExit()
    {
        
    }
}
