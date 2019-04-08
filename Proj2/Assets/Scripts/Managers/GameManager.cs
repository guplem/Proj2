using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class GameManager : MonoBehaviour
{
    [SerializeField] public new CameraManager camera;
    [SerializeField] public PlayerManager playerManager;
    [SerializeField] public Transform startPoint;
    [SerializeField] public LayerMask groundLayer;

    //[HideInInspector] public PlayerManager playerManager;

    [HideInInspector] public AudioManager audioManager;
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
            AwakeSetup();
            Debug.Log("GameManager successfully initialized", gameObject);
        }
    }

    private void AwakeSetup()
    {
        audioManager = new AudioManager(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
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
