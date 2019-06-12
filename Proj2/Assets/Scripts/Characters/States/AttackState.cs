using System.Collections;
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

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(timeToAttack);

        character.characterProperties.internalVelocity = character.characterProperties.maxRunVelocity;

        if (character.brain.actionHold) // If the player still in range
            GameManager.Instance.playerManager.hp--;
    }

    public override void Tick(float deltaTime)
    {
        character.brain.CheckAndFlip();
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x, character.characterProperties.acceleration.y), character.characterProperties.internalVelocity);
    }

    public override void OnExit()
    {
        character.characterProperties.internalVelocity = character.characterProperties.maxWalkVelocity;

    }
}