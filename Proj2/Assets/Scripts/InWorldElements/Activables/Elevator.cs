using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : Activable
{
    [SerializeField] private float travelPointOn;
    [SerializeField] private float travelPointOff;
    private float targetPoint;
    private float velocity = 0.02f;


    public override ActivationType GetActivationType()
    {
        return ActivationType.Other;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        targetPoint = state ? travelPointOn : travelPointOff;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(new Vector3(transform.position.x, travelPointOn, 0f), 0.2f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, travelPointOff, 0f), 0.2f);
    }

    private void FixedUpdate()
    { 

        float distance = targetPoint - transform.position.y;

        if (Mathf.Abs(distance) < velocity)
            return;

        transform.position = new Vector3(transform.position.x, transform.position.y + (velocity*Mathf.Sign(distance)), transform.position.z);

    }
}
