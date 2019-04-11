using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
public class ActivableDoor : Activable
{

    [SerializeField] private Collider2D doorCollider;


    protected override void SetState(bool state, CharacterManager characterActivating)
    {
        doorCollider.enabled = state;

        if (state)
            GetComponent<SpriteRenderer>().color = Color.white;
        else
            GetComponent<SpriteRenderer>().color = Color.gray;
    }

}