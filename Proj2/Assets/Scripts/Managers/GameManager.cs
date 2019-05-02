using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
[RequireComponent(typeof(AudioController))]
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

    public CheckPoint lastCheckPoint { get; private set; }

    public Action<int> ResetElementsUntilLastCheckPoint;

    public bool gamePaused { get; private set; }

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

    internal void SetPause(bool state)
    {
        gamePaused = state;
        GUIManager.Instance.PausePanel.SetObjectActive(state);
        GUIManager.Instance.ControlsPanel.SetObjectActive(state);
    }

    internal void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponent<AudioController>();
        camera.Setup(playerManager.gameObject, 0.05f);
        StartGame();
    }


    public void StartGame()
    {
        Debug.Log("Starting game");
        lastCheckPoint = null;
    }

    public void HitPlayer()
    {
        PlayerDead();
    }

    private void PlayerDead()
    {
        Debug.Log("Player is dead");

        try
        {
            SpawnPlayer(lastCheckPoint.respawnPoint.position);

            if (ResetElementsUntilLastCheckPoint != null)
                ResetElementsUntilLastCheckPoint(lastCheckPoint.zone);
        }
        catch (NullReferenceException)
        {
            SpawnPlayer(startPoint.position);

            if (ResetElementsUntilLastCheckPoint != null)
                ResetElementsUntilLastCheckPoint(-1);
        }


    }

    private void SpawnPlayer(Vector3 position)
    {
        Debug.Log("Spawning player at " + position);
        playerManager.transform.position = position;
    }

    public void CheckPointReached(CheckPoint checkPoint)
    {
        if (lastCheckPoint == null || checkPoint.zone > lastCheckPoint.zone)
        {
            lastCheckPoint = checkPoint;

            HideMainMenu();
        }
    }

    private void HideMainMenu()
    {
        GUIManager.Instance.MainMenuPanel.SetObjectActive(false);
        GUIManager.Instance.ControlsPanel.SetObjectActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SetPause(!gamePaused);
        }
    }

}
