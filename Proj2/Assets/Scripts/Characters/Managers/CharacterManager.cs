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


    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioController audioController;

    //[HideInInspector] protected Interactable currentInteractable;
    [SerializeField] public InteractionsColliderController interactionsController;

    [SerializeField] public CharacterProperties characterProperties;

    public MovementController movementController;

    public Brain defaultBrain;
    public Brain brain;

    public BehaviourTree defaultBehaviourTree;
    public BehaviourTree behaviourTree;

    // Note: the default state is given by the current behaviour tree
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

        State.SetState(this.behaviourTree.defaultState, this);

        // characterProperties = Instantiate(characterProperties); //To create a copy to debug
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


}
