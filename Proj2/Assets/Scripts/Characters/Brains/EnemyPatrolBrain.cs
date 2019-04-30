using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class EnemyPatrolBrain : Brain
{

    private Vector2[] patrolPoints;
    private int patrolPointIndex;
    private Vector2 currentPatrolPoint;

    public EnemyPatrolBrain(CharacterManager characterManager, Vector2[] patrolPoints)
    {
        base.Setup(characterManager);
        this.patrolPoints = patrolPoints;
    }

    public override void GetActions()
    {
        jumping = false;
        SetInteractingTo(false);
        action = false;
        crouch = false;

        UpdateCurrentPatrolPoint();

        direction = (currentPatrolPoint - ((Vector2)character.transform.position)).normalized;
    }

    private void UpdateCurrentPatrolPoint()
    {
        if (Vector2.Distance( character.transform.position, currentPatrolPoint) <= 1.5f)
        {
            patrolPointIndex++;
        }

        if (patrolPointIndex >= patrolPoints.Length)
        {
            patrolPointIndex = 0;
        }

        currentPatrolPoint = patrolPoints[patrolPointIndex];
    }
}
