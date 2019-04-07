using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementController
{
    CharacterManager characterManager { get; set; }
    void MoveTowards(Vector2 direction, Vector2 velocity);
    void JumpForce(float force);
    void JumpImpulse(float force);
}
