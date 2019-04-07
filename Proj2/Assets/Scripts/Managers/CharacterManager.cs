using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterManager : MonoBehaviour, ICharacterManager
{

    [HideInInspector] public IMovementController movementController { get; set; }
    [HideInInspector] public IActionsController inputController { get; set; }
    [HideInInspector] public IBehaviourTree behaviourTree { get; set; }

    [HideInInspector] public Rigidbody2D rb2d { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    [HideInInspector] public AudioManager audioManager { get; set; }

    [SerializeField] public Collider2D groundCollider;
    [SerializeField] public Collider2D topCollider;
    [SerializeField] public Collider2D lateralCollider;
    [SerializeField] public CharacterProperties characterProperties;

    [HideInInspector] public IState defaultState;
    [HideInInspector] public IState state;



    protected void Setup(IState defaultState, CharacterManager characterManager)
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // characterProperties = Instantiate(characterProperties); //To create a copy
        this.defaultState = defaultState;
        ChangeState(defaultState, characterManager);
    }

    public void Update()
    {
        state.Tick(Time.deltaTime);
        CheckTransition(false, this);
    }

    public void FixedUpdate()
    {
        state.FixedTick(Time.deltaTime);
    }

    public bool CheckTransition(bool forceExitState, CharacterManager characterManager)
    {
        return ChangeState( behaviourTree.GetNextState(forceExitState) , characterManager);
    }

    public bool ChangeState(IState newState, CharacterManager characterManager)
    {
        //Exit old
        if (state != null)
        {
            state.OnExitState();
        }

        //Change
        state = newState;

        //Initialize new
        if (state != null)
        {
            state.Initialize(characterManager);
            return true;
        }
        else
        {
            return false;
        }
        
    }


}
