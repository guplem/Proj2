﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : IState
{
    public CharacterManager characterManager { get; set; }

    public IState Initialize(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
        return this;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        characterManager.movementController.MoveTowards(new Vector2(characterManager.inputController.horizontalAxis, 0), new Vector2(characterManager.characterProperties.acceleration.x, characterManager.characterProperties.acceleration.y) );
    }

    public void Tick(float deltaTime)
    {
        characterManager.inputController.ReadInput();
    }

}