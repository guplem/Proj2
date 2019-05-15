﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
public class ActivableBox : Activable
{
    // [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask interactablesLayer;

    Rigidbody2D rb2d;

    public override ActivationType GetActivationType()
    {
        return ActivationType.Movable;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        if (characterActivating != null)
            if (state == true)
            {
                /*transform.SetParent(characterActivating.transform);
                State.SetState(new PushPullState(characterActivating), characterActivating);
                rb2d.bodyType = RigidbodyType2D.Kinematic;*/
            }
            else
            {
                /*transform.SetParent(null);
                characterActivating.behaviourTree.CalculateAndSetNextState(true);
                rb2d.bodyType = RigidbodyType2D.Dynamic;*/
            }
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

}
