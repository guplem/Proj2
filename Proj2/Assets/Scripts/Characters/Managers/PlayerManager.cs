using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168
#pragma warning disable CS0649
public class PlayerManager : CharacterManager
{
    [HideInInspector] public InventoryController inventory { get; private set; }
    [HideInInspector] public StressController stressController { get; private set; }

    [Header("Player Only")]
    [SerializeField] private Transform throwPoint;
    [SerializeField] public Vector2 throwingForce;
    [SerializeField] public float stressThreshold;

    private void Start()
    {

        base.Setup(new CharacterMovementController(this), new PlayerInput(this), new PlayerChillBehaviourTree(new WalkingState(this), this) );


        //Particular of the player
        inventory = new InventoryController(this);
        stressController = new StressController(this, stressThreshold);
    }

    protected new void Update()
    {
        base.Update();

        if (brain.action)
        {
            if (inventory.HasStoredItem())
            {
                inventory.ThrowStoredItem(new Vector2(throwingForce.x, throwingForce.y), throwPoint.position);
            }
        }
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
