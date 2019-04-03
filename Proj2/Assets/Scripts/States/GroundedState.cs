using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : IState
{
    public ICharacterManager characterManager { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Initialize()
    {
        
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.GoTo(new Vector2(characterManager.inputController.horizontalAxis, 0));
    }


    public void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
