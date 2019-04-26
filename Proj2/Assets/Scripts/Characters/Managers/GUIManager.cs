using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

#pragma warning disable 0649
public class GUIManager : MonoBehaviour
{

    
    public FadingImg BackgroundVignette { get => backgroundVignette; private set => backgroundVignette = value; }
    [SerializeField] private FadingImg backgroundVignette;
    public FadingImg PausePanel { get => pausePanel; private set => pausePanel = value; }
    [SerializeField] private FadingImg pausePanel;
    public FadingImg MainMenuPanel { get => mainMenuPanel; private set => mainMenuPanel = value; }
    [SerializeField] private FadingImg mainMenuPanel;
    public FadingImg ControlsPanel { get => controlsPanel; private set => controlsPanel = value; }
    [SerializeField] private FadingImg controlsPanel;

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
        GameManager.Instance.SetPause(false);
    }

    public void ExitGameButton()
    {
        GameManager.Instance.ExitGame();
    }

}