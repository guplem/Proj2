using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLight : Activable
{
    [SerializeField] private GameObject lightEmmiter;
    [SerializeField] private float alertRadius;
    [SerializeField] private Vector2 alertPoint;
    protected override void SetState(bool state, CharacterManager characterActivating)
    {
        lightEmmiter.SetActive(state);

        if (state)
        {
            Alertable.AlertAllInRadius(new Vector2(transform.position.x, transform.position.y) + alertPoint, alertRadius);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + alertPoint, alertRadius);
    }



}
