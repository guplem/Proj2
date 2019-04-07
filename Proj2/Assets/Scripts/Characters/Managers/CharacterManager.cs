using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour, ICharacterManager
{
    [SerializeField] public Collider2D groundCollider;
    [SerializeField] public Collider2D topCollider;
    [SerializeField] public Collider2D lateralCollider;
    [SerializeField] public CharacterProperties characterProperties;

    [HideInInspector] public IMovementController movementController { get; set; }
    [HideInInspector] public Brain brain { get; set; }

    [HideInInspector] public IBehaviourTree defaultBehaviourTree { get; set; }
    [HideInInspector] public IBehaviourTree behaviourTree { get; set; }

    [HideInInspector] public Rigidbody2D rb2d { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    [HideInInspector] public AudioManager audioManager { get; set; }

    [HideInInspector] public IState state { get; set; }


    protected void Setup(IMovementController movementController, Brain actionController, IBehaviourTree defaultBehaviourTree, AudioManager audioManager)
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


}
