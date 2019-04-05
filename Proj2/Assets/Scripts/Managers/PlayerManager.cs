using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{

    [SerializeField] private Collider2D groundCollider;
    [SerializeField] private LayerMask groundLayer;
    private ContactFilter2D groundFilter;


    private void Start()
    {
        base.Setup();

        groundFilter = new ContactFilter2D();
        groundFilter.SetLayerMask(groundLayer);

        audioManager = new AudioManager(gameObject);

        movementController = new PlayerMovementController().Initialize(this);
        inputController = new PlayerInput();
        // characterProperties = Instantiate(characterProperties);
        defaultState = new GroundedState();
        ChangeState(defaultState, this);
    }

    private void Update()
    {
        // inputController.ReadInput();
        state.Tick(Time.deltaTime);
        CheckTransition(false);
    }

    private void FixedUpdate()
    {
        state.FixedTick(Time.deltaTime);
    }

    public override bool CheckTransition(bool forceExitState)
    {
        //base.CheckTransition(forceExitState);
        if (forceExitState)
        {
            ChangeState(null, this);
        }
        if (state is GroundedState)
        {
            if (inputController.jumping)
            {
                //Check transitions
                return ChangeState(new JumpingState(), this);
            }
            //ToDo - Run?
        }
        if (!(state is JumpingState))
        {
            Collider2D[] results = new Collider2D[1];
            if (groundCollider.OverlapCollider(groundFilter, results) > 0)
            {
                return ChangeState(new GroundedState(), this);
            }
            else
            {
                return ChangeState(new OnAirState(), this);
            }
        }
        return false;
    }
}
