using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    [SerializeField] private GameObject backgroundVignette;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;

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
    }

    public void PauseMenu(bool show)
    {
        pausePanel.SetActive(show);
        Controls(show);
    }

    public void Controls(bool show)
    {
        pausePanel.SetActive(show);
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