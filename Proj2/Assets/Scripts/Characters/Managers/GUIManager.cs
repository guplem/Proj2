using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

#pragma warning disable 0649
public class GUIManager : MonoBehaviour
{

    [SerializeField] private FadingInOutImg backgroundVignette;
    [SerializeField] private FadingInOutImg pausePanel;
    [SerializeField] private FadingInOutImg mainMenuPanel;
    [SerializeField] private FadingInOutImg controlsPanel;

    //[SerializeField] private TextMeshProUGUI goLeftButton, goRightButton, jumpButton, crouchButton, interactButton, throwButton;

    [HideInInspector] private Image backgroundVignetteImg;

    [HideInInspector] private static GUIManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There are multiple instances of GUIManager.", gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        backgroundVignetteImg = backgroundVignette.GetComponent<Image>();

        //UpdateControlsButtonsText();
    }

    public void PauseMenu(bool show)
    {
        pausePanel.SetObjectActive(show);
        Controls(show);
    }

    public void Controls(bool show)
    {
        controlsPanel.SetObjectActive(show);
    }

    public void ExitGame()
    {
        //TODO
        Debug.Log("Clicked exit game button");
    }

    public void SetVignetteOpacity(float opacityPercentage)
    {
        //TODO
    }

}