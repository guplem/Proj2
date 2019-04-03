using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    IInputController inputController { get; }
    void Initialize(IInputController inputController, IMovementController movementController);
    void Tick(float deltaTime);
    void FixedTick(IMovementController movementController, float deltaTime);
}
