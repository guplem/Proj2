using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementController
{
    CharacterManager characterManager { get; set; }
    void MoveTowards(Vector2 direction, Vector2 velocity, Vector2 maxVelocity);
    void Jump(float impulse, ForceMode2D forceMode);
}
