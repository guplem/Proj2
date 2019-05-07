using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchedState : State
{

    public CrouchedState(CharacterManager characterManager)
    {
        this.character = characterManager;

        characterManager.standingCollider.enabled = false;
        characterManager.crouchCollider.enabled = true;

        Utils.ChangeGameObjectAndChildsLayerTo(character.gameObject, 100);
    }

    public override void StartState()
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x * 0.5f, character.characterProperties.acceleration.y * 0.5f), character.characterProperties.maxRunVelocity);

    }

    public override void Tick(float deltaTime)
    {

    }

    public override void OnExit()
    {
        character.standingCollider.enabled = true;
        character.crouchCollider.enabled = false;

        Utils.ChangeGameObjectAndChildsLayerTo(character.gameObject, 150);
    }
}
