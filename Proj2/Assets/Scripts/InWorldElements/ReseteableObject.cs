using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ReseteableObject : MonoBehaviour
{

    [HideInInspector] private Vector3 initialPosition;
    [HideInInspector] private int zone;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(int zone, Vector3 initialPosition)
    {
        this.initialPosition = initialPosition;
        this.zone = zone;
        GameManager.Instance.ResetUntilLastCheckPoint += MethodToCallOnEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.ResetUntilLastCheckPoint -= MethodToCallOnEvent;
    }

    public void MethodToCallOnEvent(int lastZone)
    {
        Debug.Log("MethodToCallOnEvent being called with param zone: " + lastZone, gameObject);

        if (lastZone <= zone)
        {
            Debug.Log("Resseting object.", gameObject);
            transform.position = initialPosition;

            //TODO: if it is character set state to default state
        }
    }

}
