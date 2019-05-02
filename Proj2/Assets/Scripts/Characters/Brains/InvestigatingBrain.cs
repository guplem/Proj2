using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatingBrain : Brain
{

    private Vector2 investigatingPosition;

    public InvestigatingBrain(CharacterManager characterManager, Vector2 investigatingPosition)
    {
        base.Setup(characterManager);
        this.investigatingPosition = investigatingPosition;
    }

    protected override void GetActions()
    {
        jumping = false;
        SetInteractingTo(false);
        action = false;
        crouch = false;

        UpdateCurrentInvestigatingPoint();

        direction = (investigatingPosition - ((Vector2)character.transform.position)).normalized * 0.7f;
    }

    private void UpdateCurrentInvestigatingPoint()
    {
        if (Vector2.Distance( character.transform.position, investigatingPosition) <= 1.5f)
        {
            character.brain = character.defaultBrain;
        }

    }
}
