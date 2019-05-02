﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBrain : Brain
{

    private GameObject target;

    public ChasingBrain(CharacterManager characterManager, GameObject target)
    {
        base.Setup(characterManager);
        this.target = target;
    }

    protected override void GetActions()
    {
        jumping = false;
        SetInteractingTo(false);
        action = false;
        crouch = false;

        UpdateCurrentInvestigatingPoint();

        direction = ((Vector2)target.transform.position - (Vector2)character.transform.position).normalized;
    }

    private void UpdateCurrentInvestigatingPoint()
    {
        if (Vector2.Distance( character.transform.position, target.transform.position) <= 1.5f)
        {
            character.brain = character.defaultBrain; //Hit?
        }

    }
}
