﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBrain : Brain
{

    public GameObject target { get; private set; }

    public ChasingBrain(CharacterManager characterManager, GameObject target)
    {
        base.Setup(characterManager);
        this.target = target;
    }

    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        action = character.IsNextToPosition(new Vector2(target.transform.position.x, character.transform.position.y), deltaTime, 0.5f); // (Vector2.Distance(character.transform.position, target.transform.position) <= 1.5f);
        
        crouch = false;

        if (character.IsNextToPosition(new Vector2(target.transform.position.x, character.transform.position.y), deltaTime, 0f))
            direction = Vector2.zero;
        else
            direction = ((Vector2)target.transform.position - ((Vector2)character.transform.position)).normalized;
    }

    internal void LostTrackOfTarget()
    {
        Vector2[] pointToCheck = new Vector2[1];
        pointToCheck[0] = target.transform.position + ( (target.transform.position - character.transform.position).normalized * 6f );

        Brain.SetBrain(new EnemyPatrolBrain(character, pointToCheck), 0f, character); // To keep going on the last known player direction for some time

        Brain.SetBrain(character.defaultBrain, 3.5f, character);
    }
}
