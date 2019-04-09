using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public bool singleActivation { get; set; }

    public virtual void OnEndInteract(CharacterManager interactingCharacter)
    {
        Debug.LogWarning("Called interactable base class", gameObject);
    }

    public virtual void OnStartInteract(CharacterManager interactingCharacter)
    {
        Debug.LogWarning("Called interactable base class", gameObject);
    }
}
