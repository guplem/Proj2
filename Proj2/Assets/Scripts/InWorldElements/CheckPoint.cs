using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
#pragma warning disable 0649
public class CheckPoint : MonoBehaviour
{
    [SerializeField] public int zone;
    [SerializeField] public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerManager>() != null)
            GameManager.Instance.CheckPointReached(this);
    }


}
