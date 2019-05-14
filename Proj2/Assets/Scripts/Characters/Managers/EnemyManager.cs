using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public abstract class EnemyManager : CharacterManager
{
    [Header("Player's detection configuration")]
    [SerializeField] private Vector2 lookingStartPoint;
    [Range(0f, 3.14159265359f * 2)]
    [SerializeField] private float lookingDirection;
    [Range(2, 50)]
    [SerializeField] private int lookingRaysQty;
    [Range(0f, 3.14159265359f * 2)]
    [SerializeField] private float lookingConeSize;
    [Range(2f, 50f)]
    [SerializeField] private float lookingDistance;

    protected new void Setup(MovementController movementController, Brain defaultBrain, BehaviourTree defaultBehaviourTree)
    {
        base.Setup(movementController, defaultBrain, defaultBehaviourTree);
    }

    protected void OnDrawGizmosSelected()
    {
        Vector2 pos = GetPosForStartSearching();
        //Gizmos.DrawSphere(pos, 0.2f);

        Vector2[] pointsToLookAt = GetPointsToLookAt(pos);
        for (int i = 0; i < pointsToLookAt.Length; i++)
            Gizmos.DrawLine(pos, pointsToLookAt[i] );

    }

    public void LookForPlayer(bool state)
    {
        if (state)
            InvokeRepeating("SearchPlayer", UnityEngine.Random.Range(0.5f, 1.5f), UnityEngine.Random.Range(0.5f, 1f));
        else
            CancelInvoke("SearchPlayer");
    }

    private void SearchPlayer()
    {
        if (CanSeePlayer())
        {
            if (brain.GetType() != typeof(ChasingBrain) ||
                ((ChasingBrain)brain).target != GameManager.Instance.playerManager.gameObject)
            {
                Brain nBrain = new ChasingBrain(this, GameManager.Instance.playerManager.gameObject);
                Brain.SetBrain(nBrain, 0, this);
            }
        }
        else if (brain is ChasingBrain)
        {
            ((ChasingBrain)brain).LostTrackOfTarget();
        }
    }

    public bool CanSeePlayer()
    {
        RaycastHit2D hit;

        Vector2 pos = GetPosForStartSearching();
        Vector2[] pointsToLookAt = GetPointsToLookAt(pos);

        for (var i = 0; i < pointsToLookAt.Length; i++)
        {
            Vector2 direction = ((pointsToLookAt[i] - pos).normalized);

            hit = Physics2D.Raycast(pos, direction, lookingDistance);
            if (hit.collider != null)
            {
                var player = hit.collider.GetComponent<PlayerManager>();
                if (player != null)
                { 
                    //Hitted the player
                    Debug.DrawRay(pos, direction * hit.distance, Color.red, 0.5f);
                    return true;
                }
                else
                {
                    //Hitted something that is not the player
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow, 0.5f);
                }
            }
            else
            {
                //Nothing hitted
                Debug.DrawRay(pos, direction * lookingDistance, Color.white, 0.5f);
            }
        }
        return false;
    }

    private Vector2 GetPosForStartSearching()
    {
        return new Vector2(transform.position.x, transform.position.y) + ( lookingStartPoint * GetFlip());
    }

    private Vector2 GetFlip()
    {
        return new Vector2(Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad), 1f/*Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad)*/);
    }

    private Vector2[] GetPointsToLookAt(Vector2 pos)
    {
        Vector2[] returnVectors = new Vector2[lookingRaysQty];

        for (int i = 0; i < lookingRaysQty; i++)
        {
            float rayAngle = (lookingDirection - lookingConeSize / 2) + i * (lookingConeSize / lookingRaysQty);
            returnVectors[i] = pos + new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle)) * lookingDistance * GetFlip();
        }

        return returnVectors;
    }



}
