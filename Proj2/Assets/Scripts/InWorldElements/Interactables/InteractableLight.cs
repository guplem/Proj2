using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLight : Activable
{

    protected override void ForceSetState(bool state)
    {
        if (state)
            GetComponent<SpriteRenderer>().color = Color.yellow;
        else
            GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
