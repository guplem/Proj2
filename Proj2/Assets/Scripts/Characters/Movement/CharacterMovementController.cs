using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : IMovementController
{
    public CharacterManager characterManager { get; set; }

    public CharacterMovementController(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
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

    public void CrouchedMovement(Vector2 direction, Vector2 speed)
    {
        characterManager.rb2d.AddForce(direction * speed, ForceMode2D.Force);

        // TODO Crouched movement, if it behaves diferently from the walking movement. Not only the speed.
    }
}
