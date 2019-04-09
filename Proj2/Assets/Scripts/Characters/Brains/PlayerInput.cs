using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Brain
{

    public PlayerInput(CharacterManager characterManager)
    {
        base.characterManager = characterManager;
    }

    public override void GetActions()
    {
        jumping = Input.GetButton("Jump");
        SetInteractState(Input.GetButton("Interact"));
        action = Input.GetButtonDown("Action");
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        crouch = Input.GetButton("Crouch");
    }
}
