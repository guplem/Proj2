using System.Collections;
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
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

}
