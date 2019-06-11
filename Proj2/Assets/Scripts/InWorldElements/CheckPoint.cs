using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
#pragma warning disable 0649
public class CheckPoint : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] public int zone;
    [SerializeField] public Transform respawnPoint;
    [HideInInspector] public static int checkPointsNumber { get; private set; }

    [SerializeField] private bool FinalCheckPoint;

    private void Awake()
    {
        checkPointsNumber++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager pm = collision.gameObject.GetComponent<PlayerManager>();
        if (pm != null)
        {
            if (pm.state is DeadState || pm.state is ReviveState)
                return;

            GameManager.Instance.CheckPointReached(this);
        }
    }

    public bool IsFinalCheckpoint()
    {
        return FinalCheckPoint;
    }

}
