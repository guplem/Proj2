using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : CharacterManager
{
    [SerializeField] private Vector2 lookingStartPoint;
    [Range(0f, 3.14159265359f * 2)]
    [SerializeField] private float lookingDirection;
    [Range(2, 50)]
    [SerializeField] private int lookingRaysQty;
    [Range(0f, 3.14159265359f * 2)]
    [SerializeField] private float lookingConeSize;
    [Range(2f, 50f)]
    [SerializeField] private float lookingDistance;

    protected void OnDrawGizmosSelected()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y) + lookingStartPoint;
        Gizmos.DrawSphere(pos, 0.2f);

        Vector2[] pointsToLookAt = GetPointsToLookAt();
        for (int i = 0; i < pointsToLookAt.Length; i++)
            Gizmos.DrawLine(pos, pointsToLookAt[i]);

    }

    public bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector2[] pointsToLookAt = GetPointsToLookAt();
        for (var i = 0; i < pointsToLookAt.Length; i++)
        {
            if (Physics.Raycast(transform.position, pointsToLookAt[i], out hit, lookingDistance))
            {
                var player = hit.collider.GetComponent<PlayerManager>();
                if (player != null)
                {
                    //Hitted the player
                    //Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    return true;
                }
                else
                {
                    //Hitted something that is not the player
                    //Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                //Nothing hitted
                //Debug.DrawRay(transform.position, direction * lookingDistance, Color.white);
            }
        }
        return false;
    }

    private Vector2[] GetPointsToLookAt()
    {
        Vector2[] returnVectors = new Vector2[lookingRaysQty];

        Vector2 pos = new Vector2(transform.position.x, transform.position.y) + lookingStartPoint;
        for (int i = 0; i < lookingRaysQty; i++)
        {
            float rayAngle = (lookingDirection - lookingConeSize / 2) + i * (lookingConeSize / lookingRaysQty);
            returnVectors[i] = pos + new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle)) * lookingDistance;
        }

        return returnVectors;
    }

    public void LookForPlayer()
    {
        if (CanSeePlayer())
        {
            brain = new ChasingBrain(this, GameManager.Instance.playerManager.gameObject);
        }
    }
}
