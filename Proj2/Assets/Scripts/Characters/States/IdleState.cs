﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    public IdleState(CharacterManager character)
    {
        this.character = character;
    }

    public override void StartState()
    {
        character.visualsAnimator.SetTrigger("Idle");
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {
        
    }

    public override void OnExit()
    {
        
    }


}