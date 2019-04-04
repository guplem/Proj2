using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZoneManager : MonoBehaviour
{

    [SerializeField] private int zone;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            ReseteableObject reseteableObject = child.GetComponent<ReseteableObject>();
            if (reseteableObject != null)
            {
                reseteableObject.Setup(zone, child.transform.position);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            Debug.Log("Player entered zone " + zone);
        }
    }




}
