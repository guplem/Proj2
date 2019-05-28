﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable CS0168 // Variable is declared but never used
[RequireComponent(typeof(AudioController))]
public class GameManager : MonoBehaviour
{
    [Header("Game's Configuration")]
    [SerializeField] public new CameraManager camera;
    [SerializeField] public PlayerManager playerManager;
    [HideInInspector] public Vector3 startPoint;

    [Header("Layers")]
    [SerializeField] public LayerMask walkableLayers;
    [SerializeField] public LayerMask interactableLayers;
    [SerializeField] public LayerMask playerLayers;
    [SerializeField] public LayerMask enemyLayers;

    [HideInInspector] public AudioController audioController;
    [HideInInspector] public Cursor cursor;


    [HideInInspector] public LineManager lineManager;
    [HideInInspector] public LineRenderer lineRenderer;

    [Header("Sounds")]
    public Sound backgroundSound;

    //[HideInInspector] public PlayerManager playerManager;

    public CheckPoint lastCheckPoint { get; private set; }

    public Action<int> ResetElementsUntilLastCheckPoint;

    public bool gamePaused { get; private set; }

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Trying to create a second GameManager", gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public IEnumerator SetPause(bool state)
    {
        GUIManager.Instance.PausePanel.SetObjectActive(state);
        // Select the proper element on the menu
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GUIManager.Instance.defaultPauseSelectedItem);

        yield return 0; //Aviud wrong actions by delaying one frame

        gamePaused = state;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponent<AudioController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineManager = new LineManager();
        camera.Setup(playerManager);
        lastCheckPoint = null;
        cursor = GetComponent<Cursor>();
        startPoint = playerManager.transform.position;

        audioController.PlaySound(backgroundSound, true, false);
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
            SpawnPlayer(startPoint);

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

            if (checkPoint.zone+1 >= CheckPoint.checkPointsNumber)
            {
                Application.Quit();
                Debug.Log("GAME FINISHED");
            }

            HideMainMenu();
        }
    }

    private void HideMainMenu()
    {
        GUIManager.Instance.MainMenuPanel.SetObjectActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StartCoroutine(SetPause(!gamePaused));
        }
    }

}
