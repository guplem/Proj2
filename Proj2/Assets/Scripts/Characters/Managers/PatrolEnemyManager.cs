using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class PatrolEnemyManager : EnemyManager
{

    [SerializeField] public Vector2[] patrolPoints;

    public void Start()
    {
        Configure();
        LookForPlayer(true);
    }

    public override void Configure()
    {
        BehaviourTree bt = new PatrolEnemyBehaviourTree(this, new WalkingState(this));
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

    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

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

    }


}