using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : IMovementController
{
    public CharacterManager characterManager { get; set; }

    public PlayerMovementController Initialize(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        return this;
    }

    public void JumpForce(float force)
    {
        characterManager.rb2d.AddForce(Vector2.up * force, ForceMode2D.Force);
    }

    public void JumpImpulse(float force)
    {
        characterManager.rb2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    public void MoveTowards(Vector2 direction, Vector2 acceleration)
    {
        characterManager.rb2d.AddForce(direction * acceleration, ForceMode2D.Force);

        float clampedSpeedX = Mathf.Clamp(characterManager.rb2d.velocity.x, -characterManager.characterProperties.maxVelocity.x, characterManager.characterProperties.maxVelocity.x);
        float clampedSpeedY = Mathf.Clamp(characterManager.rb2d.velocity.y, -characterManager.characterProperties.maxVelocity.y, characterManager.characterProperties.maxVelocity.y);

        characterManager.rb2d.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
    }
}
