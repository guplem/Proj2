﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : State
{

    public JumpingState(CharacterManager characterManager)
    {
        this.character = characterManager;
        // timeToStopJumping = Time.time + characterManager.characterProperties.jumpTime;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Jump");

        character.movementController.Jump(character.characterProperties.jumpForce, ForceMode2D.Impulse);

        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        /*if (Time.time >= timeToStopJumping || !characterManager.brain.jumping)
        {
            //characterManager.ChangeState(new OnAirState(), characterManager);
            characterManager.behaviourTree.SetNextState(true);
        }*/
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        //characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y*0.5f));
        //character.behaviourTree.CalculateAndSetNextState(true);
    }

    public override void OnExit()
    {
        Debug.Log("Exiting jump. Vel: " + character.rb2d.velocity.y);
    }
}