using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class EnemyManager : ChasingEnemy
{

    [SerializeField] public Vector2[] patrolPoints;
    [HideInInspector] public int patrolPointsIndex;

    private void Start()
    {
        BehaviourTree bt = new PatrolEnemyBehaviourTree(new WalkingState(this), this);
        base.Setup(new CharacterMovementController(this), new EnemyPatrolBrain(this), bt);
    }

    public new void Update()
    {
        base.Update();
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
