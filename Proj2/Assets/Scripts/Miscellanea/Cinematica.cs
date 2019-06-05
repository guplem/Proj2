using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0649
public class Cinematica : MonoBehaviour
{
    [SerializeField] private String sceneNameToLoad;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExitScene((float) gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length));
    }

    private IEnumerator ExitScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
    }
}
