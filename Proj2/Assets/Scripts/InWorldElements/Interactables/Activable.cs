using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour, IActivable
{
    public bool singleActivation { get; set; }

    public virtual void Interact(bool state)
    {
        Debug.LogError("Called the parent class", gameObject);
    }


}
