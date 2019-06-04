﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class Elevator : Activable
{
    [SerializeField] private float travelPointOn;
    [SerializeField] private float travelPointOff;
    private float targetPoint;
    private float velocity = 0.02f;
    private GameObject characterActivating;


    public override ActivationType GetActivationType()
    {
        return ActivationType.Other;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        if (state)
            targetPoint = targetPoint == travelPointOff ? travelPointOn : travelPointOff;
        else if (targetPoint != travelPointOn && targetPoint != travelPointOff)
            targetPoint = defaultState ? travelPointOn : travelPointOff;

        try
        {
            this.characterActivating = characterActivating.gameObject;
            this.characterActivating.transform.parent = gameObject.transform;
        }
        catch (System.NullReferenceException) { }
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

        transform.position = new Vector3(transform.position.x, transform.position.y + (velocity * Mathf.Sign(distance)), transform.position.z);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (characterActivating != null && collision.gameObject == characterActivating)
        {
            characterActivating.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (characterActivating != null && collision.gameObject == characterActivating)
        {
            characterActivating.transform.parent = gameObject.transform;
        }
    }
}
