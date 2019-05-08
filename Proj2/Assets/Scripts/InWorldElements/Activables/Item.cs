using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : Activable
{

    [HideInInspector] public Rigidbody2D rb2d { get; private set; }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        if (characterActivating is PlayerManager)
            ((PlayerManager)characterActivating).inventory.StoreItem(this);
    }

    public override ActivationType GetActivationType()
    {
        return ActivationType.Pickable;
    }
}