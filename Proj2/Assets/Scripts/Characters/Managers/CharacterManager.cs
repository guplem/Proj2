using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioController))]
public abstract class CharacterManager : MonoBehaviour
{
    [SerializeField] public Collider2D groundCollider;
    //[SerializeField] public Collider2D topCollider;
    //[SerializeField] public Collider2D lateralCollider;
    [SerializeField] public Collider2D standingCollider;
    [SerializeField] public Collider2D crouchCollider;
    //[SerializeField] public Collider2D interactionsCollider;

    [SerializeField] public CharacterProperties characterProperties;

    public MovementController movementController;

    public Brain defaultBrain;
    public Brain brain;

    public BehaviourTree defaultBehaviourTree;
    public BehaviourTree behaviourTree;

    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioController audioController;

    [HideInInspector] protected Interactable currentInteractable;

    public State state;

    protected void Setup(MovementController movementController, Brain defaultBrain, BehaviourTree defaultBehaviourTree)
    {
        this.movementController = movementController;

        this.defaultBrain = defaultBrain;
        this.brain = this.defaultBrain;

        this.defaultBehaviourTree = defaultBehaviourTree;
        this.behaviourTree = this.defaultBehaviourTree;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = GetComponent<AudioController>();

        this.state = this.behaviourTree.defaultState;

        // characterProperties = Instantiate(characterProperties); //To create a copy
    }

    protected void Update()
    {
        brain.Act();

        behaviourTree.CalculateAndSetNextState(false);

        state.Tick(Time.deltaTime);
    }

    protected void FixedUpdate()
    {
        state.FixedTick(Time.fixedDeltaTime);
    }

    

    public void OnInterectableTriggerEnter(Collider2D collision)
    {
        // Is an interactable object
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null && collision.isTrigger)
        {
            if (interactable.interactAutomatically)
            {
                interactable.StartInteract(this);
                return;
            }

            //Currently not interacting with any object
            if (!brain.interact)
            {
                currentInteractable = interactable;
            }
        }
    }

    public void OnInterectableTriggerExit(Collider2D collision)
    {
        // Is an interactable object
        Interactable interactable = collision.GetComponent<Interactable>();
        if (interactable != null && collision.isTrigger)
        {

            if (interactable.interactAutomatically)
            {
                interactable.EndInteract(this);
                return;
            }

            //Currently not interacting with any object
            if (!brain.interact)
            {
                // If exiting the current interactable
                if (currentInteractable == interactable)
                    currentInteractable = null;
            }
        }
    }

    public void ProcessNewInteractState(bool isInteractionBegining)
    {
        if (currentInteractable != null)
        {
            if (isInteractionBegining)
            {
                currentInteractable.StartInteract(this);
            }
            else
            {
                currentInteractable.EndInteract(this);
            }

        }
    }


}
