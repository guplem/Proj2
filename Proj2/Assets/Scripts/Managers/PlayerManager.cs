using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    private void Start()
    {
        base.Setup();

        audioManager = new AudioManager();

        movementController = new PlayerMovementController().Initialize(this);
        inputController = new PlayerInput();
        // characterProperties = Instantiate(characterProperties);
        defaultState = new GroundedState();
        ChangeState(defaultState, this);
    }

    private void Update()
    {
        // inputController.ReadInput();
        state.Tick(Time.deltaTime);
        CheckTransition();
    }

    private void FixedUpdate()
    {
        state.FixedTick(Time.deltaTime);
    }

    private void CheckTransition()
    {
        if (inputController.jumping)
        {
            //Check transitions
        }
    }
}
