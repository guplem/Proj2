using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class PlayerManager : CharacterManager
{
    InventoryController inventory;
    private List<CharacterManager> chasedBy = new List<CharacterManager>();

    private void Start()
    {
        base.Setup();

        inventory = new InventoryController();
        defaultState = new GroundedState();
        ChangeState(defaultState, this);

        audioManager = new AudioManager(gameObject);
        
        movementController = new PlayerMovementController().Initialize(this);
        inputController = new PlayerInput();
        behaviourTree = new PlayerBehaviourTree();

    }

    public new void Update()
    {
        base.Update();
    }

    public new void FixedUpdate()
    {
        base.Update();
    }

    public new bool CheckTransition(bool forceExitState)
    {
        return base.CheckTransition(forceExitState);
    }

    public PlayerManager AddChasing(CharacterManager cm)
    {
        chasedBy.Add(cm);
        return this;
    }
    public PlayerManager RemoveChasing(CharacterManager cm)
    {
        chasedBy.Remove(cm);
        return this;
    }
}
