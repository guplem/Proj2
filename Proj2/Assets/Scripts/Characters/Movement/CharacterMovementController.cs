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

    public void Jump(float force, ForceMode2D forceMode)
    {
        characterManager.rb2d.AddForce(Vector2.up * force, forceMode);
    }


    public void MoveTowards(Vector2 direction, Vector2 acceleration, Vector2 maxVelocity)
    {
        characterManager.rb2d.AddForce(direction * acceleration, ForceMode2D.Impulse);

        float clampedSpeedX = Mathf.Clamp(characterManager.rb2d.velocity.x, -maxVelocity.x, maxVelocity.x);
        float clampedSpeedY = Mathf.Clamp(characterManager.rb2d.velocity.y, -maxVelocity.y, maxVelocity.y);

        characterManager.rb2d.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
    }

    public void CrouchedMovement(Vector2 direction, Vector2 speed)
    {
        characterManager.rb2d.AddForce(direction * speed, ForceMode2D.Force);

        // TODO Crouched movement, if it behaves diferently from the walking movement. Not only the speed.
    }
}
