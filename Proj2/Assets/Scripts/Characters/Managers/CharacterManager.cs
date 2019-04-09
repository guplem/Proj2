using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour, ICharacterManager
{
    [SerializeField] public Collider2D groundCollider;
    [SerializeField] public Collider2D topCollider;
    [SerializeField] public Collider2D lateralCollider;
    [SerializeField] public Collider2D standingCollider;
    [SerializeField] public Collider2D crouchCollider;

    [SerializeField] public CharacterProperties characterProperties;

    [HideInInspector] public IMovementController movementController { get; set; }
    [HideInInspector] public IBrain brain { get; set; }

    [HideInInspector] public IBehaviourTree defaultBehaviourTree { get; set; }
    [HideInInspector] public IBehaviourTree behaviourTree { get; set; }

    [HideInInspector] public Rigidbody2D rb2d { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    [HideInInspector] public AudioManager audioManager { get; set; }

    [HideInInspector] public IState state { get; set; }

    [HideInInspector] public Interactable currentInteractable { get; set; }

    public int lookingDirection { get; set; }

    //[HideInInspector] public GameObject currentInteractableGameObject { get; set; }


    /*public Action<int> StartInteract;
    public Action<int> EndInteract;*/


    protected void Setup(IMovementController movementController, IBrain actionController, IBehaviourTree defaultBehaviourTree, AudioManager audioManager)
    {
        this.movementController = movementController;
        this.brain = actionController;

        this.defaultBehaviourTree = defaultBehaviourTree;
        this.behaviourTree = this.defaultBehaviourTree;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.audioManager = audioManager;

        this.state = this.behaviourTree.defaultState;

        // characterProperties = Instantiate(characterProperties); //To create a copy
    }

    public void Update()
    {
        brain.GetActions();

        behaviourTree.SetNextState(false);

        state.Tick(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        state.FixedTick(Time.fixedDeltaTime);

        if (Mathf.Abs(rb2d.velocity.x) < 0.1)
        {
            if (brain.direction.x != 0)
                lookingDirection = brain.direction.x > 0 ? 1 : -1;
        } else
        {
            if (rb2d.velocity.x != 0)
                lookingDirection = rb2d.velocity.x > 0 ? 1 : -1;
        }

    }

    public void SetState(IState newState)
    {
        if (newState != null && state != null)
        {
            if (state.GetType() != newState.GetType())
            {
                state = newState;
            }
        }
        else
        {
            state = newState;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //currentInteractableGameObject = collision.gameObject;
        Interactable collInteract = collision.GetComponent<Interactable>();
        if (collInteract != null)
        {
            currentInteractable = collInteract;
            Debug.Log("Setting interactable:" + currentInteractable);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //currentInteractableGameObject = collision.gameObject;
        Interactable collInteract = collision.GetComponent<Interactable>();
        if (collInteract == currentInteractable)
            return;
        if (collInteract != null)
        {
            currentInteractable = collInteract;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //currentInteractableGameObject = collision.gameObject;
        Interactable collInteract = collision.GetComponent<Interactable>();
        if (collInteract != null)
        {
            if (currentInteractable == collInteract)
            {
                currentInteractable.OnEndInteract(this);
                currentInteractable = null;
            }
        }
    }

    public void UpdateInteractState(bool isInteractionStart)
    {
        if (currentInteractable != null)
        {
            if (isInteractionStart)
            {
                currentInteractable.OnStartInteract(this);
            }
            else
            {
                currentInteractable.OnEndInteract(this);
            }

        }
    }


}
