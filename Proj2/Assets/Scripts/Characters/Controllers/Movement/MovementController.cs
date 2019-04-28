using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementController
{
    protected CharacterManager character;

    protected void Setup(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public abstract void MoveTowards(Vector2 direction, Vector2 accelerationVelocity, Vector2 maxVelocity);
    public abstract void Jump(float impulse, ForceMode2D forceMode);

}