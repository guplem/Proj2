using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[SelectionBase]
[RequireComponent(typeof(Collider2D))]
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

    }

    public void StartInteract(CharacterManager interactingCharacter)
    {
        if (RegisterAndAskForStartInteraction())
        {
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
                Interact(interactingCharacter);
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
        if (animator != null)
            animator.SetBool("Active", !animator.GetBool("Active"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interactAutomatically)
            return;

        InteractionsColliderController interactionController = collision.GetComponent<InteractionsColliderController>();
        if (interactionController != null)
        {
            StartInteract(interactionController.character);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!interactAutomatically)
            return;

        InteractionsColliderController interactionController = collision.GetComponent<InteractionsColliderController>();
        if (interactionController != null)
        {
            EndInteract(interactionController.character);
        }
    }
}
