using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICharacterManager
{
    public IMovementController movementController => throw new System.NotImplementedException();

    public IInputController inputController => throw new System.NotImplementedException();

    public Rigidbody2D rb2d => throw new System.NotImplementedException();

    public Animator animator => throw new System.NotImplementedException();

    public AudioManager audioManager => throw new System.NotImplementedException();

    public IState state;

    // private 

    private void Start()
    {

    }

    private void Update()
    {
        state.Tick( Time.deltaTime);
        CheckTransition();
        inputController.ReadInput();
    }

    private void FixedUpdate()
    {
        state.FixedTick(movementController, Time.deltaTime);
    }

    private void CheckTransition()
    {
        if (inputController.jumping)
        {
            //Check transitions
        }
    }

    private void ChangeState(IState newState)
    {
        state = newState;
    }
}
