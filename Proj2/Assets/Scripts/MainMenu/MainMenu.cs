using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

#pragma warning disable 0649
public class MainMenu : MonoBehaviour
{

    [SerializeField] private string gameScene;
    [SerializeField] private GameObject defaultSelectedObject;
    [SerializeField] private Fading black;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelectedObject);

        StartCoroutine(DisableBlackScreen());

        if (!Application.isEditor)
            UnityEngine.Cursor.visible = false;
    }

    private IEnumerator DisableBlackScreen()
    {
        yield return new WaitForEndOfFrame();
        black.SetObjectActive(false);

    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("ExitGame");
    }

    public void PlayButton()
    {
        black.SetObjectActive(true);

        StartCoroutine(LoadSceneAfterTime(black.fadeTimeDuration, gameScene));
    }

    private IEnumerator LoadSceneAfterTime(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
