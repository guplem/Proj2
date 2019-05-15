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

    bool waitingToExitBrain = false;
    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        action = false;
        crouch = false;

        if (!character.IsNextToPosition(investigatingPosition, deltaTime, 0.3f))
            direction = (investigatingPosition - ((Vector2)character.transform.position)).normalized;
        else if (!waitingToExitBrain)
        {
            Debug.Log("Set default brain");
            SetBrain(character.defaultBrain, 2f, character);
            waitingToExitBrain = true;
        }
    }

}
