using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICharacterManager
{
    public IMovementController movementController => throw new System.NotImplementedException();

    public IInputController inputController => throw new System.NotImplementedException();

    public IState state;

    // private 

    private void Start()
    {

    }

    private void Update()
    {
        state.Tick(inputController, movementController, Time.deltaTime);
        CheckTransition();
    }

    private void FixedUpdate()
    {
        state.FixedTick(inputController, movementController, Time.deltaTime);
    }

    private void CheckTransition()
    {
        if (inputController)
        {

        }
    }

    private void ChangeState(IState newState)
    {
        state = newState;
    }
}
