using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{
    IMovementController movementController { get; }
    IInputController inputController { get; }

    Rigidbody2D rb2d { get; }
    Animator animator { get; }
    AudioManager audioManager { get; }
}
