﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class PatrolEnemyManager : EnemyManager
{
    [Header("Patrol configuration")]
    [SerializeField] public Vector2[] patrolPoints;

    public void Start()
    {
        Configure();
        LookForPlayer(true);
    }

    public override void Configure()
    {
        BehaviourTree bt = new PatrolEnemyBehaviourTree(this, new IdleState(this));
        base.Setup(new CharacterMovementController(this), new EnemyPatrolBrain(this, patrolPoints), bt);
    }

    public new void Update()
    {
        base.Update();
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected void OnDrawGizmos()
    {
        foreach (Vector2 point in patrolPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point, 0.2f);
        }

        for (int p = 0; p < patrolPoints.Length-1; p++)
        {
            try
            {
                Gizmos.DrawLine(patrolPoints[p], patrolPoints[p + 1]);
            }
            catch (IndexOutOfRangeException)
            { }
        }

        if (brain != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position+brain.direction);
        }

        if (brain is EnemyPatrolBrain)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(((EnemyPatrolBrain)brain).currentPatrolPoint, 0.3f);
        }

        if (brain is InvestigatingBrain)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(((InvestigatingBrain)brain).investigatingPosition, 0.3f);
        }

        
    }

    public override IEnumerator Alert(Vector2 position)
    {
        yield return new WaitForSeconds(0.1f);
        /*Vector2 selfPosition = new Vector2(transform.position.x, transform.position.y);
        if (brain is InvestigatingBrain)
        {
            if ((((InvestigatingBrain)brain).investigatingPosition - selfPosition).magnitude < (position - selfPosition).magnitude)
            {
                yield return;
            }
        }*/
        Brain.SetBrain(new InvestigatingBrain(this, position), 0.2f, this, true);
    }
}