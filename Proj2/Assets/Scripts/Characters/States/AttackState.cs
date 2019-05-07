﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float timeToAttack;

    public AttackState(CharacterManager characterManager, float loadingTime)
    {
        this.character = characterManager;
        timeToAttack = loadingTime;
    }

    public override void StartState()
    {

    }

    public override void Tick(float deltaTime)
    {
        timeToAttack -= deltaTime;

        if (timeToAttack <= 0)
        {
            if (character.brain.action) // If the player still in range
            {
                GameManager.Instance.HitPlayer();
            }
        }
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, character.characterProperties.acceleration.y), character.characterProperties.maxWalkVelocity);
    }

    public override void OnExit()
    {
        
    }
}