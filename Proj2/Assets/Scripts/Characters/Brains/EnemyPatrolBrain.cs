using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class EnemyPatrolBrain : Brain
{
    
    public bool jumping { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool interact { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool action { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2 direction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public EnemyManager characterManager;

    public EnemyPatrolBrain(EnemyManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void GetActions()
    {
        throw new System.NotImplementedException();
    }
}
