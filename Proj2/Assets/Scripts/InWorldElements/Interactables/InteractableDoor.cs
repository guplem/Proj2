using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
public class InteractableDoor : Activable
{
    //Animation sound?
    [SerializeField] private Collider2D doorCollider;

    public override void Interact(bool state)
    {
        //Play required animation, sound or etc.
        if (doorCollider != null)
        {
            if (singleActivation)
            {
                doorCollider.enabled = false;
            }
            else
            {
                doorCollider.enabled = !doorCollider.enabled;
            }
        }
        else
        {
            Debug.LogWarning("No Door collider attached!", gameObject);
        }
        Debug.Log("Opened door with " + state, gameObject);
    }
}
