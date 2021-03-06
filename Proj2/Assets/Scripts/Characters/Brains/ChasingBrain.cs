﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBrain : Brain
{

    public CharacterManager target { get; private set; }

    public ChasingBrain(CharacterManager characterManager, CharacterManager target)
    {
        base.Setup(characterManager);
        this.target = target;
    }

    protected override void GetActions(float deltaTime)
    {
        if (target.hp <= 0)
        {
            Brain.SetBrain(character.defaultBrain, 0f, character, false);
            return;
        }


        character.characterProperties.internalVelocity = character.characterProperties.maxRunVelocity;

        jumping = false;
        interact = false;
        actionHold = character.IsNextToPosition(target.transform.position, deltaTime, 1.5f); //(Vector2.Distance(character.transform.position, target.transform.position) <= 1.5f);
        crouch = false;

        if (character.IsNextToPosition(target.transform.position, deltaTime, 0.5f))
            direction = Vector2.zero;
        else
            //direction = ((Vector2)target.transform.position - ((Vector2)character.transform.position)).normalized;
            direction = (target.transform.position.x < character.transform.position.x) ? Vector2.left : Vector2.right;
    }

    internal void LostTrackOfTarget()
    {
        Vector2[] pointToCheck = new Vector2[1];
        pointToCheck[0] = target.transform.position + ( (target.transform.position - character.transform.position).normalized * 6f );

        Brain.SetBrain(new EnemyPatrolBrain(character, pointToCheck), 0f, character, true); // To keep going on the last known player direction for some time

        Brain.SetBrain(character.defaultBrain, 3.5f, character, false);
    }
}
