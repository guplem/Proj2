using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class InteractionsColliderController : MonoBehaviour
{

    [SerializeField] public CharacterManager character;
    [HideInInspector] private Collider2D col;
    //private Interactable currentInteractable;



    private void Start()
    {
        if (character == null)
            Debug.LogError("'CharacterManager' is not setted up in the 'InteractionsColliderController' of the object '" + gameObject.name + "'", gameObject) ;

        col = GetComponent<Collider2D>();
    }

    public Interactable GetAvaliableInterectable(Activable.ActivationType activationType)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(GameManager.Instance.interactablesLayer);
        Collider2D[] results = new Collider2D[10];
        int collidersDetected = col.OverlapCollider(filter, results);

        if (collidersDetected >= results.Length)
            Debug.LogWarning("The number of colliders being checked while trying to interact can be more than " + results.Length + ". Consider increasing the 'results' array length or decreasing the colliders at the area.");

        Debug.Log("Colliders detected: " + collidersDetected + " with " + col.ToString() + " of " + character.gameObject.name);

        foreach (Collider2D col in results)
        {
            if (col == null)
                continue;

            Interactable interactable = col.GetComponent<Interactable>();

            foreach (Activable activable in interactable.connectedActivables)
            {
                if (activable.GetActivationType() == activationType)
                    return interactable;
            }
        }
        return null;
    }



    /*private void OnTriggerEnter2D(Collider2D collision)
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
    }*/

}