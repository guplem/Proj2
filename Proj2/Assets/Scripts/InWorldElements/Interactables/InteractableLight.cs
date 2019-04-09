using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLight : Activable
{
    
    public override void Interact(bool isOn)
    {
        //TODO: turn on/off the light

        //For visual testing: 
        if (isOn)
            GetComponent<SpriteRenderer>().color = Color.yellow;
        else
            GetComponent<SpriteRenderer>().color = Color.gray;
    }

}
