using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchedState : IState
{
    public CharacterManager characterManager { get; set; }
    public CharacterManager playerManager { get; set; }

    public CrouchedState(PlayerManager characterManager)
    {
        this.characterManager = characterManager;
        this.playerManager = characterManager;

        characterManager.standingCollider.enabled = false;
        characterManager.crouchCollider.enabled = true;

        Utils.ChangeGameObjectAndChildsLayerTo(playerManager.gameObject, 100);
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.brain.direction.x, 0), new Vector2(characterManager.characterProperties.acceleration.x * 0.5f, characterManager.characterProperties.acceleration.y * 0.5f), characterManager.characterProperties.maxRunVelocity);

    }

    public void Tick(float deltaTime)
    {

    }

    public void OnExit()
    {
        characterManager.standingCollider.enabled = true;
        characterManager.crouchCollider.enabled = false;

        Utils.ChangeGameObjectAndChildsLayerTo(playerManager.gameObject, 150);
    }
}
