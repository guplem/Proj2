﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    public CharacterManager characterManager { get; set; }

    private float timeToStopJumping;

    public JumpingState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        timeToStopJumping = Time.time + characterManager.characterProperties.jumpTime;
    }

    public void Tick(float deltaTime)
    {
        if (Time.time >= timeToStopJumping || !characterManager.brain.jumping)
        {
            //characterManager.ChangeState(new OnAirState(), characterManager);
            characterManager.behaviourTree.SetNextState(true);
        }
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.JumpForce(characterManager.characterProperties.jumpForce);
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y*0.5f));
    }

}