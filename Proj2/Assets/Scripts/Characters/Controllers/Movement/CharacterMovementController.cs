using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MovementController
{
    public CharacterMovementController(CharacterManager characterManager)
    {
        base.Setup(characterManager);
    }

    public override void Jump(float force, ForceMode2D forceMode)
    {
        character.rb2d.AddForce(Vector2.up * force, forceMode);
    }


    public override void MoveTowards(Vector2 direction, Vector2 acceleration, Vector2 maxVelocity)
    {
        character.rb2d.AddForce(direction * acceleration, ForceMode2D.Impulse);

        float clampedSpeedX = Mathf.Clamp(character.rb2d.velocity.x, -maxVelocity.x, maxVelocity.x);
        float clampedSpeedY = Mathf.Clamp(character.rb2d.velocity.y, -maxVelocity.y, maxVelocity.y);

        character.rb2d.velocity = new Vector2(clampedSpeedX, clampedSpeedY);
    }

}