﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[SelectionBase]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioController))]
public class Interactable : MonoBehaviour
{
    [Header("Interaction configuration")]
    [SerializeField] private InteractType interactType;
    [SerializeField] public bool interactAutomatically;
    [SerializeField] public bool singleInteraction;

    [HideInInspector] public bool alreadyStartedInteraction;
    [HideInInspector] public bool alreadyEndedInteraction;

    [Header("Visuals")]
    //[SerializeField] private GameObject initialVisuals;
    //[SerializeField] private GameObject interactingVisuals;
    [SerializeField] private Animator animator;

    [Header("Activable elements")]
    [SerializeField] public Activable[] connectedActivables;
    [SerializeField] private float activationDelay = 0;

    [SerializeField] private Sound startInteractSound, endInteractSound;
    [SerializeField] public Sound usingSound; //Using sound does not play automatically, it is meant to be reproduced by other objects that are using the activable
    private AudioController audioController;

    public enum InteractType
    {
        Button, 
        Switch
    }

    private void Awake()
    {
        alreadyStartedInteraction = false;
        alreadyEndedInteraction = false;

        //Security checks
        if (connectedActivables.Length == 0)
            Debug.LogWarning("No activables attatched to an interactable.", gameObject);

        /*if (initialVisuals == null ^ interactingVisuals == null)
            Debug.LogWarning("Only one of the visuals is configured in an interactable object.", gameObject);

        if (initialVisuals != null)
            initialVisuals.SetActive(true);
        if (interactingVisuals != null)
            interactingVisuals.SetActive(false);*/

        if (animator != null)
            animator.SetBool("Active", false);

        audioController = GetComponent<AudioController>();
        if (audioController == null && (startInteractSound != null || endInteractSound != null) )
            Debug.LogWarning("No audio controller found for " + gameObject.name, gameObject);

    }

    public IEnumerator StartInteract(CharacterManager interactingCharacter)
    {
        if (RegisterAndAskForStartInteraction())
        {
            if (audioController != null)
                audioController.PlaySound(startInteractSound, false, false);

            if (animator != null)
                animator.SetBool("Active", !animator.GetBool("Active"));

            yield return new WaitForSeconds(activationDelay);
            Interact(interactingCharacter);
        }
    }

    //registers the interaction and Returns true if the interaction can be started.
    private bool RegisterAndAskForStartInteraction()
    {
        bool toReturn = ((singleInteraction && !alreadyStartedInteraction) || !singleInteraction);

        alreadyStartedInteraction = true;

        return toReturn;
    }

    public void EndInteract(CharacterManager interactingCharacter)
    {
        if (RegisterAndAskForEndInteraction())
        {
            if (interactType == InteractType.Button)
            {
                Interact(interactingCharacter);
                if (audioController != null)
                    audioController.PlaySound(endInteractSound, false, false);
            }
        }
    }

    //Returns true if the interaction can be finished and registers the interaction
    private bool RegisterAndAskForEndInteraction()
    {
        bool toReturn = ((singleInteraction && !alreadyEndedInteraction) || !singleInteraction);

        alreadyEndedInteraction = true;

        return toReturn;
    }


    private void Interact(CharacterManager interactingCharacter)
    {
        foreach (Activable activable in connectedActivables)
        {
            activable.SwitchState(interactingCharacter);
        }


        /*if (initialVisuals != null)
            initialVisuals.SetActive(!initialVisuals.activeSelf);
        if (interactingVisuals != null)
            interactingVisuals.SetActive(!interactingVisuals.activeSelf);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactAutomatically)
            return;

        InteractionsController interactionController = collision.GetComponent<InteractionsController>();
        if (interactionController != null)
        {
            StartCoroutine( StartInteract(interactionController.character) );
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!interactAutomatically)
            return;

        InteractionsController interactionController = collision.GetComponent<InteractionsController>();
        if (interactionController != null)
        {
            EndInteract(interactionController.character);
        }
    }
}
