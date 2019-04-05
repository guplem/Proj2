using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    public CharacterManager characterManager { get; set; }

    private float timeToStopJumping;

    public IState Initialize(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        timeToStopJumping = Time.time + characterManager.characterProperties.jumpTime;
        return this;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.JumpForce(characterManager.characterProperties.jumpForce);
        characterManager.movementController.MoveTowards(new Vector2(characterManager.inputController.horizontalAxis, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y*0.5f));
    }

    public void Tick(float deltaTime)
    {
        characterManager.inputController.ReadInput();
        if (Time.time >= timeToStopJumping || !characterManager.inputController.jumping)
        {
            //characterManager.ChangeState(new OnAirState(), characterManager);
            characterManager.CheckTransition(true);
        }
    }
}

