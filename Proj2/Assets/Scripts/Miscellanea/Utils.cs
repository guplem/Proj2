
using System;
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

    internal static Vector3 GetClosestPointToCircle(Vector3 center, float areaRadius, Vector3 point)
    {
        if (Vector3.Distance(point, center) < areaRadius)
            return point;

        Vector3 V = (point - center);
        return center + V / V.magnitude * areaRadius;
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

    public static void SetObjectLookingDirection(int dir, GameObject gameObject)
    {
        if (dir == 1)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);

            if (dir != -1)
                Debug.LogWarning("The direction given to the 'SetObjectLookingDirection' method should be '1' (right) or '-1' (legt) and you are tying to set it as '" + dir + "'.", gameObject);
        }
    }

    public static IEnumerator LowerVolumeAndStopSounds(AudioSource audioSource)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.12f);

        }
        audioSource.Stop();
        audioSource.volume = 1.0f;
    }

}