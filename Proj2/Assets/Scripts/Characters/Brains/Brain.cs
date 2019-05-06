using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain
{
    public bool jumping;
    public bool interact;
    public bool action;
    public bool actionRelease;
    public bool crouch;
    public Vector2 direction;
    protected CharacterManager character;

    public void Act()
    {
        GetActions();
        CheckAndFlip();
    }

    protected abstract void GetActions();

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

    protected void CheckAndFlip()
    {
        if (direction.x >= 0.1f)
        {
            character.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x < -0.1f)
        {
            character.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }


}