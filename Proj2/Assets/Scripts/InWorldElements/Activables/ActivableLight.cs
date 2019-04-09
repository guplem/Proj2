using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLight : Activable
{

    protected override void ForceSetState(bool state, CharacterManager characterActivating)
    {
        if (state)
            GetComponent<SpriteRenderer>().color = Color.yellow;
        else
            GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
