using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Animator))]
public class ActivableDoor : Activable
{

    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private Animator animator;

    public override ActivationType GetActivationType()
    {
        return ActivationType.Other;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        doorCollider.enabled = !state;
        animator.SetBool("Open", state);
    }

}