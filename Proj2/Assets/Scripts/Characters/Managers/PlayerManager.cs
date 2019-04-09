using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class PlayerManager : CharacterManager
{
    [HideInInspector] public InventoryController inventory;
    [HideInInspector] private List<CharacterManager> chasedBy;
    [SerializeField] public Vector2 throwingForce;
    [SerializeField] private Transform throwPoint;

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

        if (brain.action)
        {
            if (inventory.HasStoredItem())
            {
                inventory.storedItem.gameObject.SetActive(true);
                ThrowItem(inventory.storedItem);
                inventory.ClearStoredItem();
            }
        }
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

    private void ThrowItem(Item item)
    {
        item.gameObject.transform.position = throwPoint.position;

        Vector2 dir = new Vector2(throwingForce.x * lookingDirection, throwingForce.y);
        item.rb2d.AddForce(dir, ForceMode2D.Impulse);
    }
}
