using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ZoneManager : MonoBehaviour
{
    [SerializeField] private int zoneNumber;
    //[SerializeField] private CheckPointObject checkPoint;

    private void Awake()
    {
        List<GameObject> objectsInZone = new List<GameObject>();
        Utils.SaveAllChilds(gameObject, objectsInZone);

        //objectsInZone.Add(checkPoint.gameObject);

        // Surely unnecessary
        objectsInZone.Add(gameObject);

        foreach (GameObject obj in objectsInZone)
        {
            ReseteableObject reseteableObject = obj.GetComponent<ReseteableObject>();
            if (reseteableObject != null)
            {
                reseteableObject.Setup(zoneNumber, obj.transform.position);
            }
        }
    }

}