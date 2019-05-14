﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBrain : Brain
{

    private Vector2[] patrolPoints;
    private int patrolPointIndex;
    public Vector2 currentPatrolPoint { get; private set; }

    public EnemyPatrolBrain(CharacterManager characterManager, Vector2[] patrolPoints)
    {
        base.Setup(characterManager);
        this.patrolPoints = patrolPoints;

        if (patrolPoints.Length <= 0)
        {
            Debug.LogError("'patrolPoints' not initialied on a 'EnemyPatrolBrain' class.", characterManager.gameObject);
            Debug.Break();
        }
    }

    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        action = false;
        crouch = false;

        UpdateCurrentPatrolPoint(deltaTime);

        /*Vector2 pp = new Vector2(currentPatrolPoint.x, character.transform.position.y);
        direction = (pp - ((Vector2)character.transform.position)).normalized;*/

        direction = (currentPatrolPoint - ((Vector2)character.transform.position)).normalized;
    }

    private void UpdateCurrentPatrolPoint(float deltaTime)
    {
        //if (Vector2.Distance(character.transform.position, currentPatrolPoint) <= character.characterProperties.maxWalkVelocity.x * deltaTime)
        Vector2 pp = new Vector2(currentPatrolPoint.x, character.transform.position.y);
        if (character.IsNextToPosition(pp, deltaTime))
        {
            patrolPointIndex++;
        }

        if (patrolPointIndex >= patrolPoints.Length)
        {
            patrolPointIndex = 0;
        }


        currentPatrolPoint = new Vector2(patrolPoints[patrolPointIndex].x, character.transform.position.y);
    }

}
