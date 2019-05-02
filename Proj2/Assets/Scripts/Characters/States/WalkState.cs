﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IState
{
    public CharacterManager character { get; set; }

    public WalkingState(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public void Tick(float deltaTime)
    {
        
    }

    public void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, character.characterProperties.acceleration.y), character.characterProperties.maxWalkVelocity);
    }

    public void OnExit()
    {
        
    }
}