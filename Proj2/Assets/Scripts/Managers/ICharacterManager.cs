using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterManager
{
    IMovementController movementController { get; set; }
    IInputController inputController { get; set; }

    Rigidbody2D rb2d { get; set; }
    Animator animator { get; set; }
    AudioManager audioManager { get; set; }

    bool ChangeState(IState newState, CharacterManager characterManager);
}
