using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0649
public class Cinematica : MonoBehaviour
{
    [SerializeField] private String sceneNameToLoad;
    private float timePressed;
    public float timeToSkip;

    private IEnumerator coroutineHolder;
    // Start is called before the first frame update
    void Start()
    {

        coroutineHolder = ExitScene((float)gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().clip.length);
        StartCoroutine(coroutineHolder);
        if (!Application.isEditor)
    		UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timePressed += Time.deltaTime;
            if (timePressed >= timeToSkip)
            {
                StopCoroutine(coroutineHolder);
                SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
            }
        }
        else
        {
            timePressed = 0f;
        }

    }

    private IEnumerator ExitScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneNameToLoad, LoadSceneMode.Single);
    }
}
