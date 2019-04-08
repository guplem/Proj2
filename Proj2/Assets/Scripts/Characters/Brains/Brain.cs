using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain : IBrain
{
    public bool jumping { get; set; }
    public bool interact { get; set; }
    public bool action { get; set; }
    public Vector2 direction { get; set; }
    public CharacterManager characterManager { get; set; }

    public virtual void GetActions()
    {
        Debug.LogError("GetActions not implemented in a class that inherits from Brain", characterManager.gameObject);
    }

    public void SetInteractState(bool state)
    {
        if (interact != state)
        {
            if (state == true)
            {
                characterManager.StartInteract(0);
            }
            if (state == false)
            {
                characterManager.EndInteract(0);
            }
            interact = state;
        }
    }
}
