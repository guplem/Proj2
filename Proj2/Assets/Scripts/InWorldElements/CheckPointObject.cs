using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
#pragma warning disable 0649
public class CheckPointObject : MonoBehaviour
{
    [SerializeField] private int zone;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>() != null)
            GameManager.Instance.CheckPointReached(new CheckPoint(zone, spawnPoint.position));
    }


}
