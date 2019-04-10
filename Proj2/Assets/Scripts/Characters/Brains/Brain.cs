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
    public CharacterManager characterManager;

    public abstract void GetActions();

    public void SetInteractState(bool state)
    {
        if (characterManager == null)
        {
            Debug.LogError("characterManager not set in " + this + " (inherence of Brain class)");
        }

        if (interact != state)
        {
            characterManager.UpdateInteractState(state);
            interact = state;
        }
    }
}
