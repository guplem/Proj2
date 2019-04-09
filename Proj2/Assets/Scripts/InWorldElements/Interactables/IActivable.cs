using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivable
{
    bool singleActivation { get; set; }
    void Interact(bool state);
}
