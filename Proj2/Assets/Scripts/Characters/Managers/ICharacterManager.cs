using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{
    IMovementController movementController { get; set; }
    Brain brain { get; set; }

    BehaviourTree defaultBehaviourTree { get; set; }
    BehaviourTree behaviourTree { get; set; }

    Rigidbody2D rb2d { get; set; }
    Animator animator { get; set; }
    AudioManager audioManager { get; set; }

    IState state { get; set; }

    void SetState(IState newState);

    Interactable currentInteractable { get; set; }

    void UpdateInteractState(bool isInteractionStart);

    int lookingDirection { get; set; }

}