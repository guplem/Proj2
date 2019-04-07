using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class PlayerManager : CharacterManager
{
    [HideInInspector] private InventoryController inventory;
    [HideInInspector] private List<CharacterManager> chasedBy;

    private void Start()
    {
        base.Setup(new GroundedState(), this);

        //Initialization of components present in all characters
        movementController = new CharacterMovementController(this);
        inputController = new PlayerInput(this);
        behaviourTree = new PlayerBehaviourTree(this);
        audioManager = new AudioManager(gameObject);

        //Particular of the player
        inventory = new InventoryController();
        chasedBy = new List<CharacterManager>();
    }

    public new void Update()
    {
        base.Update();
    }

    public new void FixedUpdate()
    {
        base.Update();
    }

    public new bool CheckTransition(bool forceExitState, CharacterManager characterManager)
    {
        return base.CheckTransition(forceExitState, characterManager);
    }

    public PlayerManager AddChasing(CharacterManager characterManager)
    {
        chasedBy.Add(characterManager);
        return this;
    }
    public PlayerManager RemoveChasing(CharacterManager characterManager)
    {
        chasedBy.Remove(characterManager);
        return this;
    }
}
