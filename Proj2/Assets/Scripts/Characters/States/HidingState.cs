using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingState : IState
{
    public CharacterManager characterManager { get; set; }

    private Vector2 exitPoint;

    public HidingState(CharacterManager characterManager, GameObject gameObject)
    {
        this.characterManager = characterManager;
        HideInObject(gameObject);
    }

    public void Tick(float deltaTime)
    {

    }

    public void FixedTick(float fixedDeltaTime)
    {

    }

    public void HideInObject(GameObject gameObject)
    {
        // TODO. This sets the player (or character) inside an object and makes it undetectable (in any form).
        this.exitPoint = gameObject.transform.position;
        characterManager.transform.position = gameObject.transform.position;
    }

    public void ExitObject()
    {
        characterManager.transform.position = exitPoint;
    }

}
