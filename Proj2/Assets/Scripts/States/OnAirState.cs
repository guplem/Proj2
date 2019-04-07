using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : IState
{
    public CharacterManager characterManager { get; set; }

    public IState Initialize(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        return this;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.inputController.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y * 0.5f));
    }

    public void Tick(float deltaTime)
    {
        characterManager.inputController.ReadInput();
    }

    public void OnExitState()
    {
        Debug.LogWarning("OnExitState  not implemented on OnAirState", characterManager.gameObject);
    }

}
