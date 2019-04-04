﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IInputController
{

    public bool jumping { get; set; }
    public bool interact { get; set; }
    public bool action { get; set; }
    public float horizontalAxis { get; set; }
    public float verticalAxis { get; set; }

    public void ReadInput()
    {
        jumping = Input.GetButton("Jump");
        interact = Input.GetButton("Interact");
        action = Input.GetButton("Action");
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
    }
}
