using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class EnemyPatrolBrain : Brain
{

    private Vector2[] patrolPoints;

    public EnemyPatrolBrain(CharacterManager characterManager, Vector2[] patrolPoints)
    {
        base.Setup(characterManager);
        this.patrolPoints = patrolPoints;
    }

    public override void GetActions()
    {
        throw new System.NotImplementedException();
    }
}
