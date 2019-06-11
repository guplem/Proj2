using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

#pragma warning disable 0649
public class GUIManager : MonoBehaviour
{
    
    public Fading BackgroundVignette { get => backgroundVignette; private set => backgroundVignette = value; }
    [SerializeField] private Fading backgroundVignette;
    public Fading PausePanel { get => pausePanel; private set => pausePanel = value; }
    [SerializeField] private Fading pausePanel;
    //public Fading MainMenuPanel { get => mainMenuPanel; private set => mainMenuPanel = value; }
    //[SerializeField] private Fading mainMenuPanel;  
    public Fading InventoryImage { get => inventoryPanel; private set => inventoryPanel = value; }
    [SerializeField] private Fading inventoryPanel;

    public Fading DeathScreenPanel { get => deathScreenPanel; private set => deathScreenPanel = value; }
    [SerializeField] private Fading deathScreenPanel;

    [Header("Pause configuration")]
    [SerializeField] public GameObject defaultPauseSelectedItem;

    [SerializeField] public string exitScene;

    [HideInInspector] public static GUIManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There are multiple instances of GUIManager.", gameObject);
        else
            Instance = this;
    }

    public void ResumeGameButton()
    {
        StartCoroutine(GameManager.Instance.SetPause(false));
    }

    public void ExitGameButton()
    {
        SceneManager.LoadScene(exitScene, LoadSceneMode.Single);
    }

    public void SetInventoryImage(Sprite image)
    {
        if (image == null)
        {
            inventoryPanel.SetObjectActive(false);
        }
        else
        {
            inventoryPanel.GetComponent<Image>().sprite = image;
            inventoryPanel.SetObjectActive(true);
        }
    }

}