using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{
    IMovementController movementController { get; set; }
    IBrain brain { get; set; }

    IBehaviourTree defaultBehaviourTree { get; set; }
    IBehaviourTree behaviourTree { get; set; }

    Rigidbody2D rb2d { get; set; }
    Animator animator { get; set; }
    AudioManager audioManager { get; set; }

    IState state { get; set; }

    void SetState(IState newState);
}
