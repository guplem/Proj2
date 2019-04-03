using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ReseteableObject : MonoBehaviour
{

    private Vector3 initialPosition;
    [SerializeField] int zone;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        GameManager.Instance.ResetUntilLastCheckPoint += MethodToCallOnEvent;
    }

    public void MethodToCallOnEvent(int lastZone)
    {
        Debug.Log("MethodToCallOnEvent being called with param zone: " + lastZone, gameObject);

        if (lastZone <= zone)
        {
            Debug.Log("Resseting object.", gameObject);

            //TODO: Resset
            //transform.position = initialPosition; // TBD: enough?
        }
    }
}
