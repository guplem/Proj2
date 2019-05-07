using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class InteractionsColliderController : MonoBehaviour
{

    [SerializeField] private CharacterManager character;
    private Interactable currentInteractable;

    private void Start()
    {
        if (character == null)
            Debug.LogWarning("'CharacterManager' is not setted up in the 'InteractionsColliderController' of the object '" + gameObject.name + "'", gameObject) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable == null || !collision.isTrigger)
            return;

        // Is an interactable object

        if (interactable.interactAutomatically)
        {
            interactable.StartInteract(character);
            return;
        }

        //Currently not interacting with any object
        if (!character.brain.interact)
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable == null || !collision.isTrigger)
            return;

        if (interactable.interactAutomatically)
        {
            interactable.EndInteract(character);
            return;
        }

        //Currently not interacting with any object
        if (!character.brain.interact)
        {
            // If exiting the current interactable
            if (currentInteractable == interactable)
                currentInteractable = null;
        }
    }

    public void ProcessNewInteractState(bool isInteractionBegining)
    {
        if (currentInteractable != null)
        {
            if (isInteractionBegining)
            {
                currentInteractable.StartInteract(character);
            }
            else
            {
                currentInteractable.EndInteract(character);
            }

        }
    }

}