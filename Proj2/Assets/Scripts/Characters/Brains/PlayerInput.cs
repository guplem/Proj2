﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Brain
{

    public PlayerInput(CharacterManager characterManager)
    {
        base.Setup(characterManager);
    }

    protected override void GetActions()
    {
        jumping = Input.GetButton("Jump");
        interact = Input.GetButton("Interact");
        action = Input.GetButton("Action");
        actionRelease = Input.GetButtonUp("Action");
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        crouch = Input.GetButton("Crouch");
    }
}
