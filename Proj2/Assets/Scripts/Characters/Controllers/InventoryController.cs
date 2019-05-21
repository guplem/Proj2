using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    public Item storedItem
    {
        get
        {
            return _storedItem;
        }

        private set
        {
            if (value == null)
                GUIManager.Instance.SetInventoryImage(null);
            else
                GUIManager.Instance.SetInventoryImage(value.GetComponent<SpriteRenderer>().sprite);

            _storedItem = value;
        }
    }
    private Item _storedItem; // Must not be used, use "storedItem" instead.
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
            DropStoredItem(characterManager.transform.position);

        storedItem = item;
        item.gameObject.SetActive(false);
    }

    private void DropStoredItem(Vector3 dropPosition)
    {
        storedItem.gameObject.SetActive(true);
        storedItem.transform.position = dropPosition;
    }

    internal void ThrowStoredItem(Vector2 forceAndDirection, Vector3 throwPosition)
    {
        storedItem.gameObject.SetActive(true);

        storedItem.transform.position = throwPosition;

        storedItem.rb2d.AddForce(forceAndDirection, ForceMode2D.Impulse);

        ClearStoredItem();
    }
}