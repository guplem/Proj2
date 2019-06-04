using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioController))]
public abstract class CharacterManager : MonoBehaviour
{
    [Header("Character configuration")]
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
    [SerializeField] public InteractionsController interactionsController;
    [SerializeField] public Animator visualsAnimator;

    [SerializeField] public CharacterProperties characterProperties;

    public MovementController movementController;

    public Brain defaultBrain;
    public Brain brain;

    public BehaviourTree defaultBehaviourTree;
    public BehaviourTree behaviourTree;

    // Note: the default state is given by the current behaviour tree
    public State state;

    public int hp = 1;

    public abstract void Configure();
    protected void Setup(MovementController movementController, Brain defaultBrain, BehaviourTree defaultBehaviourTree)
    {
        this.movementController = movementController;

        this.defaultBrain = defaultBrain;
        this.brain = defaultBrain; //To avoid null reference exception on the next line
        Brain.SetBrain(defaultBrain, 0f, this, true);

        this.defaultBehaviourTree = defaultBehaviourTree;
        this.behaviourTree = this.defaultBehaviourTree;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = GetComponent<AudioController>();

        State.SetState(behaviourTree.defaultState, this);

        // characterProperties = Instantiate(characterProperties); //To create a copy to debug
    }

    protected void Update()
    {
        brain.Act(Time.deltaTime);

        behaviourTree.CalculateAndSetNextState(false);

        state.Tick(Time.deltaTime);
    }

    protected void FixedUpdate()
    {
        state.FixedTick(Time.fixedDeltaTime);
    }

    public bool IsTouchingGround()
    {
        return Utils.IsColliderTouchingLayer(groundCollider, GameManager.Instance.walkableLayers);
    }

    public abstract void Alert(Vector2 position);

    public bool IsNextToPosition(Vector2 position, float deltaTime, float maxDistance)
    {
        return (Vector2.Distance(transform.position, new Vector2(position.x, transform.position.y)) <= characterProperties.internalVelocity.x * deltaTime + maxDistance);
    }
}
