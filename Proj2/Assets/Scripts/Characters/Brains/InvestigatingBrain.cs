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

    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        actionHold = false;
        crouch = false;

        if (!character.IsNextToPosition(investigatingPosition, deltaTime, 0.4f))
        {
            if (investigatingPosition.x >= character.transform.position.x)
                direction = Vector2.right;
            else
                direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
            SetBrain(character.defaultBrain, 5f, character, false);
        }
    }

}
