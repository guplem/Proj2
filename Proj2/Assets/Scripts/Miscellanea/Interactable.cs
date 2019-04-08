using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterManager character = collision.GetComponent<CharacterManager>();
        if (character != null)
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CharacterManager character = collision.GetComponent<CharacterManager>();
        if (character != null)
        {

        }
    }

    public virtual void OnPlayerStartInteract()
    {
        Debug.LogError("OnPlayerStartInteract not implemented for an Interactable object", gameObject);
    }

    public virtual void OnPlayerEndInteract()
    {
        Debug.LogError("OnPlayerEndInteract not implemented for an Interactable object", gameObject);
    }

}
