using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public abstract class ChasingEnemy : CharacterManager
{
    public Vector2 pointOfInterest;

    //TODO: searches for the player. If the enemy is able of seeing him saves his position in the "pointOfInterest" variable and return his PlayerManager.
    public PlayerManager LookForPlayer()
    {
        return null;
    }

    public void DrawAttention(Vector2 pontThatDrawsAttention)
    {
        pointOfInterest = pontThatDrawsAttention;
        behaviourTree = null; //TODO: change the behaviour tree for a "Chasing enemy nehaviour tree"
    }

}
