using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : IState
{
    public ICharacterManager characterManager { get; set; }

    public void Initialize()
    {
        
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveThowards(new Vector2(characterManager.inputController.horizontalAxis, 0), new Vector2(characterManager.characterProperties.maxHorVelocity, characterManager.characterProperties.maxVerVelocity) );
    }


    public void Tick(float deltaTime)
    {
       // throw new System.NotImplementedException();
    }
}
