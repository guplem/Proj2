using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Item storedItem { get; private set; }

    public void RemoveStoredItem()
    {
        storedItem = null;
    }

    public bool HasStoredItem()
    {
        return storedItem != null;
    }

    public void StoreItem(Item item)
    {
        storedItem = item;
    }


}
