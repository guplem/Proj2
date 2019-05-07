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

    private bool cancelAction; //this bool is used to cancel the throw.

    private void Start()
    {

        base.Setup(new CharacterMovementController(this), new PlayerInput(this), new PlayerChillBehaviourTree(new WalkingState(this), this));
<<<<<<< HEAD
=======

>>>>>>> feature/Corrected_dinamic_throw

        //Particular of the player
        inventory = new InventoryController(this);

        stressController = GetComponent<StressController>();//new StressController(this, stressThreshold);
<<<<<<< HEAD

        cancelAction = false;

=======
        cancelAction = false;
>>>>>>> feature/Corrected_dinamic_throw
    }

    protected new void Update()
    {
        base.Update();

        CheckThrow();

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void CheckThrow()
    {
        if (brain.action && !cancelAction)
        {
            if (brain.interact)  //Use this button to cancel while holding the throwing button.
            {
                GameManager.Instance.lineManager.SetDrawing(false);
                cancelAction = true;
                return;
            }
            if (inventory.HasStoredItem())
            {
<<<<<<< HEAD
                inventory.ThrowStoredItem(new Vector2(throwingForce.x, throwingForce.y), throwPoint.position);
=======
>>>>>>> feature/Corrected_dinamic_throw
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                Vector3[] points = { throwPoint.position, mousePosition };

                GameManager.Instance.lineManager.SetupLinePoints(points);
                GameManager.Instance.lineManager.SetDrawing(true);
            }
            else
            {
                Debug.Log("I do not have an item stored!");
<<<<<<< HEAD
=======
            }
        }
        else if (brain.actionRelease)
        {
            if (inventory.HasStoredItem() && !cancelAction)
            {
                //inventory.ThrowStoredItem(new Vector2(throwingForce.x * lookingDirection, throwingForce.y), throwPoint.position);

                Vector3 thisVector;
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                thisVector = mousePosition - throwPoint.position;

                inventory.ThrowStoredItem(new Vector2(throwingForce.x, throwingForce.y) * thisVector, throwPoint.position);
                GameManager.Instance.lineManager.SetDrawing(false);
>>>>>>> feature/Corrected_dinamic_throw
            }
            cancelAction = false;
        }
        else if (brain.actionRelease)
        {
            if (inventory.HasStoredItem() && !cancelAction)
            {
                //inventory.ThrowStoredItem(new Vector2(throwingForce.x * lookingDirection, throwingForce.y), throwPoint.position);
                Vector3 thisVector;
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                thisVector = mousePosition - throwPoint.position;

                inventory.ThrowStoredItem(new Vector2(throwingForce.x, throwingForce.y) * thisVector, throwPoint.position);
                GameManager.Instance.lineManager.SetDrawing(false);
            }
            cancelAction = false;
        }
    }
}
