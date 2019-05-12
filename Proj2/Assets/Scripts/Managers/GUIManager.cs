using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

#pragma warning disable 0649
public class GUIManager : MonoBehaviour
{

    
    public Fading BackgroundVignette { get => backgroundVignette; private set => backgroundVignette = value; }
    [SerializeField] private Fading backgroundVignette;
    public Fading PausePanel { get => pausePanel; private set => pausePanel = value; }
    [SerializeField] private Fading pausePanel;
    public Fading MainMenuPanel { get => mainMenuPanel; private set => mainMenuPanel = value; }
    [SerializeField] private Fading mainMenuPanel;
    public Fading ControlsPanel { get => controlsPanel; private set => controlsPanel = value; }
    [SerializeField] private Fading controlsPanel;

    [Header("Pause configuration")]
    [SerializeField] public GameObject defaultPauseSelectedItem;


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
        GameManager.Instance.ExitGame();
    }

}