
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

    public static void ChangeGameObjectLayer(GameObject go, int layer)
    {
        SpriteRenderer sprnd = go.GetComponent<SpriteRenderer>();
        if (sprnd != null)
            sprnd.sortingOrder = layer;
        else
            Debug.Log("SpriteRenderer null in " + go.ToString());
    }

}
