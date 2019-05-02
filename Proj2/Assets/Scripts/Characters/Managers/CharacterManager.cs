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

    public IState state { get; private set; }

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

    public void Update()
    {
        brain.Act();

        behaviourTree.CalculateAndSetNextState(false);

        state.Tick(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        state.FixedTick(Time.fixedDeltaTime);
    }

    public void SetState(IState newState)
    {
        // If only one of both is null or neither any of both is
        if ((state == null ^ newState == null) || (state != null && newState != null))
        {
            try // To ensue that a null state gives no problems. If one of both is null an exception will be catched.
            {
                // If both are the same state
                if (state.GetType() == newState.GetType())
                    return;
            }
            catch (NullReferenceException) { }

            // If one of both is null (jumped trugh carching exception)
            // ...or...
            // Old state is not null and neither is newState but both are different
            ForceSetState(newState);
        }
    }

   

    private void ForceSetState(IState newState)
    {
        if (state != null)
            state.OnExit();

        state = newState;
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
