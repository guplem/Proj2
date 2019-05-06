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
    //[SerializeField] public float stressThreshold;

    private void Start()
    {

        base.Setup(new CharacterMovementController(this), new PlayerInput(this), new PlayerChillBehaviourTree(new WalkingState(this), this));


        //Particular of the player
        inventory = new InventoryController(this);
        stressController = GetComponent<StressController>();//new StressController(this, stressThreshold);
    }

    protected new void Update()
    {
        base.Update();

        if (brain.action)
        {
            if (inventory.HasStoredItem())
            {
                inventory.ThrowStoredItem(new Vector2(throwingForce.x, throwingForce.y), throwPoint.position);
                //inventory.ThrowStoredItem(new Vector2(throwingForce.x * lookingDirection, throwingForce.y), throwPoint.position);
                Vector3 thisVector;
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                Vector3 limit = new Vector3(0.6f, 0.5f, 0);
                //float limit2 = 10.0f;
                thisVector = mousePosition - throwPoint.position;
                //Vector3 treatedPoint = throwPoint.position + (thisVector.normalized * thisVector.magnitude);
                //treatedPoint = new Vector3(Mathf.Clamp(treatedPoint.x, -limit.x, limit.x), Mathf.Clamp(treatedPoint.y, -limit.y, limit.x), treatedPoint.z);

                inventory.ThrowStoredItem(new Vector2(throwingForce.x , throwingForce.y) * thisVector, throwPoint.position);
            }
        }
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
    }

}
