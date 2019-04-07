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
        base.Setup(new CharacterMovementController(this), new PlayerInput(this), new PlayerChillBehaviourTree(new WalkingState(this), this), new AudioManager(gameObject) );

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
        base.FixedUpdate();
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
