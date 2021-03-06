﻿using System;
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
    private PlayerManager character { get; set;}

    public InventoryController(PlayerManager characterManager)
    {
        this.character = characterManager;
    }

    private void ClearStoredItem()
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
            DropStoredItem(character.transform.position);

        storedItem = item;
        item.gameObject.SetActive(false);
    }

    public void DropStoredItem(Vector3 dropPosition)
    {
        if (storedItem != null)
        {
            storedItem.gameObject.SetActive(true);
            storedItem.transform.position = dropPosition;
            storedItem = null;
        }
    }

    internal void ThrowStoredItem(Vector2 forceAndDirection, Vector3 throwPosition)
    {
        if (storedItem == null)
        {
            Debug.LogWarning("Trying to throw a non-existent stored item", character.gameObject);
            return;
        }

        storedItem.gameObject.SetActive(true);

        storedItem.transform.position = throwPosition;

        storedItem.rb2d.AddForce(forceAndDirection, ForceMode2D.Impulse);
        storedItem.rb2d.angularVelocity = UnityEngine.Random.Range(120f, 450f);

        ClearStoredItem();
    }
}