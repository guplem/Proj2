using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatingBrain : Brain
{

    public Vector2 investigatingPosition { get; private set; }

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
        actionHold = false;
        crouch = false;

        if (!character.IsNextToPosition(investigatingPosition, deltaTime, 0.3f))
            direction = (investigatingPosition - ((Vector2)character.transform.position)).normalized;
        else if (!waitingToExitBrain)
        {
            SetBrain(character.defaultBrain, 2f, character, false);
            waitingToExitBrain = true;
        }
    }

}
