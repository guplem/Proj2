using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementController
{
    void MoveThowards(Vector2 direction, Vector2 velocity);
    void JumpForce(float force);
    void JumpImpulse(float force);
}
