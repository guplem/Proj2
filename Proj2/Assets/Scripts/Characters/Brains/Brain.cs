using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain
{
    public bool jumping;
    public bool interact;
    public bool action;
    public bool crouch;
    public Vector2 direction;
    protected CharacterManager character;

    public abstract void GetActions();

    protected void Setup(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public void SetInteractingTo(bool state)
    {
        if (character == null)
        {
            Debug.LogError("characterManager not set in " + this + " (inherence of Brain class)");
        }

        if (interact != state)
        {
            character.ProcessNewInteractState(state);
            interact = state;
        }
    }
}