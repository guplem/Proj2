using System;
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
    [HideInInspector] public Transform startPoint;

    [Header("Layers")]
    [SerializeField] public LayerMask walkableLayers;
    [SerializeField] public LayerMask interactableLayers;
    [SerializeField] public LayerMask playerLayers;
    [SerializeField] public LayerMask enemyLayers;

    [HideInInspector] public AudioController audioController;
    [HideInInspector] public Cursor cursor;


    [HideInInspector] public LineManager lineManager;
    [HideInInspector] public LineRenderer lineRenderer;

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

    internal IEnumerator SetPause(bool state)
    {
        GUIManager.Instance.PausePanel.SetObjectActive(state);
        GUIManager.Instance.ControlsPanel.SetObjectActive(state);

        // Select the proper element on the menu
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GUIManager.Instance.defaultPauseSelectedItem);

        yield return 0; //Aviud wrong actions by delaying one frame

        gamePaused = state;
    }

    internal void ExitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioController = GetComponent<AudioController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineManager = new LineManager();
        camera.Setup(playerManager.gameObject, 0.05f);
        lastCheckPoint = null;
        cursor = GetComponent<Cursor>();
        startPoint = playerManager.transform;
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
            StartCoroutine(SetPause(!gamePaused));
        }
    }

}
