using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ActivableLight : Activable
{
    [SerializeField] private GameObject[] activableObjectsOnSetActive;
    [SerializeField] private float alertRadius;
    [SerializeField] private Vector2 alertPoint;

    public override ActivationType GetActivationType()
    {
        return ActivationType.Other;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        foreach (GameObject go in activableObjectsOnSetActive)
        {
            go.SetActive(state);
        }
        if (state && alertAtActivate)
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
