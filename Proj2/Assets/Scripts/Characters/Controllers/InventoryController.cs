using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    public Item storedItem { get; private set; }
    private PlayerManager characterManager { get; set;}

    public InventoryController(PlayerManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void ClearStoredItem()
    {
        storedItem = null;
    }

    public bool HasStoredItem()
    {
        return storedItem != null;
    }

    public void StoreItem(Item item)
    {
        if (storedItem != null)
            storedItem.Drop(characterManager.transform.position);

        storedItem = item;
    }
    
}