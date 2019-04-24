using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class GameManager : MonoBehaviour
{
    [Header("Game's Configuration")]
    [SerializeField] public new CameraManager camera;
    [SerializeField] public PlayerManager playerManager;
    [SerializeField] public Transform startPoint;

    [Header("Layers")]
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask interactablesLayer;
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask worldLayer;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] public LayerMask walkableLayers;

    [HideInInspector] public AudioController audioController;

    //[HideInInspector] public PlayerManager playerManager;

    private CheckPoint lastCheckPoint;

    public Action<int> ResetUntilLastCheckPoint;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Trying to create a second GmeManager", gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponent<AudioController>();
        playerManager = playerManager.GetComponent<PlayerManager>();
        camera.Setup(playerManager.gameObject, 0.05f);
        StartGame();
    }


    public void StartGame()
    {
        Debug.Log("Starting game");
        lastCheckPoint = new CheckPoint(0, startPoint.transform.position);
    }

    private void PlayerDead()
    {
        Debug.Log("Player is dead");

        SpawnPlayer(lastCheckPoint.position);

        if (ResetUntilLastCheckPoint != null)
            ResetUntilLastCheckPoint(lastCheckPoint.zone);
    }

    private void SpawnPlayer(Vector3 position)
    {
        Debug.Log("Spawning player at " + position);
        playerManager.transform.position = position;
    }

    public void CheckPointReached(CheckPoint checkPoint)
    {
        if (checkPoint.zone > lastCheckPoint.zone)
        {
            lastCheckPoint = checkPoint;
        }
    }


}
