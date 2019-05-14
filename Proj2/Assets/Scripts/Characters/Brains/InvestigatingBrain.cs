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

    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        actionHold = false;
        crouch = false;

        if (!character.IsNextToPosition(investigatingPosition, deltaTime))
            direction = (investigatingPosition - ((Vector2)character.transform.position)).normalized * 0.7f;
        else
            SetBrain(character.defaultBrain, 2f, character);
    }

}
