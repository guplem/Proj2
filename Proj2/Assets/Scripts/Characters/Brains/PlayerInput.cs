using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Brain
{

    public bool jumping { get; set; }
    public bool interact { get; set; }
    public bool action { get; set; }
    public Vector2 direction { get; set; }
    public CharacterManager characterManager;

    public PlayerInput(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void GetActions()
    {
        jumping = Input.GetButton("Jump");
        interact = Input.GetButton("Interact");
        action = Input.GetButton("Action");
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
