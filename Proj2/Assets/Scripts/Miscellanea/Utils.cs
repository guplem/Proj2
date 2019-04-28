
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool IsColliderTouchingLayer(Collider2D collider, LayerMask layer)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layer);

        Collider2D[] results = new Collider2D[1];

        return collider.OverlapCollider(filter, results) > 0;
    }

    public static void ChangeGameObjectAndChildsLayerTo(GameObject gameObject, int layer)
    {
        ChangeGameObjectLayerTo(gameObject, layer);

        foreach (Transform child in gameObject.transform)
            ChangeGameObjectLayerTo(child.gameObject, layer);

    }

    public static void ChangeGameObjectLayerTo(GameObject gameObject, int layer)
    {
        SpriteRenderer sprnd = gameObject.GetComponent<SpriteRenderer>();
        if (sprnd != null)
            sprnd.sortingOrder = layer;
    }

    public static void SaveAllChilds(GameObject gameObject, List<GameObject> childsList)
    {
        if (gameObject == null)
            return;

        foreach (Transform child in gameObject.transform)
        {
            if (child == null)
                continue;

            childsList.Add(child.gameObject);

            SaveAllChilds(child.gameObject, childsList);
        }
    }

}