using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168
#pragma warning disable CS0649
public class PlayerManager : CharacterManager
{
    [HideInInspector] public InventoryController inventory { get; private set; }
    [HideInInspector] public StressController stressController { get; private set; }

    [Header("Player Configuration")]
    [SerializeField] private Transform throwPoint;
    [SerializeField] public Vector2 throwingForce;
    //[SerializeField] public float stressThreshold;

    private void Start()
    {
        Configure();

    }

    public override void Configure()
    {
        base.Setup(new CharacterMovementController(this), new PlayerInput(this), new PlayerChillBehaviourTree(new IdleState(this), this));

        hp = 0;
        //Particular of the player
        inventory = new InventoryController(this);

        stressController = GetComponent<StressController>();//new StressController(this, stressThreshold);
    }

    protected new void Update()
    {
        base.Update();

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public Transform getThrowPoint()
    {
        return throwPoint;
    }

    public override void Alert(Vector2 position)
    {
        // Does nothing
    }
}
