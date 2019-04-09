using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class EnemyPatrolBrain : IBrain
{
    
    public bool jumping { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool interact { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool action { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool crouch { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Vector2 direction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public CharacterManager characterManager { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public EnemyManager enemyManager;

    public EnemyPatrolBrain(EnemyManager characterManager)
    {
        this.characterManager = characterManager;
        this.enemyManager = characterManager;
    }

    public void GetActions()
    {
        throw new System.NotImplementedException();
    }

    public void SetInteractState(bool state)
    {
        throw new System.NotImplementedException();
    }
}
