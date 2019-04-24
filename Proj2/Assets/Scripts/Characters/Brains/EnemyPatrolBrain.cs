using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class EnemyPatrolBrain : Brain
{

    public PatrolEnemyManager enemyManager;

    public EnemyPatrolBrain(PatrolEnemyManager characterManager)
    {
        this.characterManager = characterManager;
        this.enemyManager = characterManager;
    }

    public override void GetActions()
    {
        throw new System.NotImplementedException();
    }
}
