using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour, ICharacterManager
{

    public IMovementController movementController { get; set; }
    public IInputController inputController { get; set; }

    public Rigidbody2D rb2d { get; set; }
    public Animator animator { get; set; }
    public AudioManager audioManager { get; set; }


    public CharacterProperties characterProperties;


    protected IState defaultState;
    protected IState state;

    public void Setup()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void ChangeState(IState newState, CharacterManager characterManager)
    {
        state = newState.Initialize(characterManager);
    }
}
