using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchedState : IState
{
    public CharacterManager characterManager { get; set; }

    public CrouchedState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        characterManager.standingCollider.enabled = false;
        characterManager.crouchCollider.enabled = true;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void Tick(float deltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y * 0.5f), characterManager.characterProperties.maxRunVelocity);
    }

    public void OnExit()
    {
        characterManager.standingCollider.enabled = true;
        characterManager.crouchCollider.enabled = false;
    }
}
