using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#pragma warning disable CS0168
#pragma warning disable CS0649
#pragma warning disable CS0109
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

    [SerializeField] private AudioMixer mechanicsAudioMixer;
    private IEnumerator startMechanicsSounds;

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

        startMechanicsSounds = StartGameplaySounds();
        StartCoroutine(startMechanicsSounds);
        audioController.PlaySound(backgroundSound, true, false);
    }



    public void RevivePlayer()
    {
        playerManager.inventory.DropStoredItem(playerManager.transform.position);

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

        playerManager.hp = 1;
        State.SetState(new ReviveState(), playerManager);
    }

    private void SpawnPlayer(Vector3 position)
    {
        playerManager.transform.position = position;
    }

    public void CheckPointReached(CheckPoint checkPoint)
    {
        if (lastCheckPoint == null || checkPoint.zone > lastCheckPoint.zone)
        {
            lastCheckPoint = checkPoint;

            if (checkPoint.zone + 1 >= CheckPoint.checkPointsNumber)
            {
                GUIManager.Instance.DeathScreenPanel.SetObjectActive(true);
                Invoke("LoadFinalCinematic", GUIManager.Instance.DeathScreenPanel.fadeTimeDuration);
                //TODO: decrease all fx volume progressively (master)
                //TODO: fix why is not loading on build?
                Debug.Log("GAME FINISHED");
            }
        }
    }

    private void LoadFinalCinematic()
    {
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }

    public void CheckPointReachedForced(CheckPoint checkPoint)
    {
        lastCheckPoint = checkPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StartCoroutine(SetPause(!gamePaused));
        }
    }

    private IEnumerator StartGameplaySounds()
    {
        mechanicsAudioMixer.SetFloat("volume", -80.0f);
        yield return new WaitForSeconds(5.0f);
        mechanicsAudioMixer.SetFloat("volume", 0.0f);
        StopCoroutine(startMechanicsSounds);
    }

}
