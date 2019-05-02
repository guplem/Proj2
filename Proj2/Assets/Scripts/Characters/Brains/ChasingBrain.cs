using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBrain : Brain
{

    public GameObject target { get; private set; }

    public ChasingBrain(CharacterManager characterManager, GameObject target)
    {
        base.Setup(characterManager);
        this.target = target;
    }

    protected override void GetActions()
    {
        jumping = false;
        SetInteractingTo(false);
        action = (Vector2.Distance(character.transform.position, target.transform.position) <= 1.5f);
        crouch = false;

        direction = ((Vector2)target.transform.position - (Vector2)character.transform.position).normalized;
    }


}
