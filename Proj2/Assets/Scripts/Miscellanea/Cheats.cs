using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// https://docs.unity3d.com/ScriptReference/MenuItem.html

public class Cheats : MonoBehaviour
{
    [MenuItem("Bunker Bound/Cheats/Hit Player #K")]
    public static void HitPlayer()
    {
        if (Application.isPlaying)
        {
            GameManager.Instance.playerManager.hp--;
        }
        else
        {
            Debug.LogError("Not in play mode.");
        }
    }

    [MenuItem("Bunker Bound/Cheats/Force reset until last checkpoint #R")]
    public static void ResetTilLastCheckpoint()
    {
        if (Application.isPlaying)
        {
            try
            {
                if (GameManager.Instance.ResetElementsUntilLastCheckPoint != null)
                    GameManager.Instance.ResetElementsUntilLastCheckPoint(GameManager.Instance.lastCheckPoint.zone);
            }
            catch (NullReferenceException)
            {
                if (GameManager.Instance.ResetElementsUntilLastCheckPoint != null)
                    GameManager.Instance.ResetElementsUntilLastCheckPoint(-1);
            }
        }
        else
        {
            Debug.LogError("Not in play mode.");
        }
    }

    [MenuItem("Bunker Bound/Cheats/Activate Highlighted Activable #T")]
    public static void ActivateHighlightedActivableObject()
    {
        if (Application.isPlaying)
        {
            try
            {
                GameObject obj = Selection.activeGameObject;
                Activable actObj = obj.GetComponent<Activable>();
                if (actObj != null)
                {
                    actObj.SwitchState(null);
                }
            }
            catch
            {
                Debug.Log("No object selected!");
            }
        }
        else
        {
            Debug.LogError("Not in play mode.");
        }
    }

    public void Update()
    {
        if (!Application.isPlaying)
            return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetCh(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetCh(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetCh(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCh(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetCh(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetCh(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetCh(6);
            }
        }
    }

    private void SetCh(int checkPointNumber)
    {
        CheckPoint[] CheckPoints = (CheckPoint[])FindObjectsOfType(typeof(CheckPoint));
        foreach (CheckPoint ch in CheckPoints)
        {
            if (ch.zone == checkPointNumber)
            {
                GameManager.Instance.CheckPointReachedForced(ch);
                Debug.Log("Forced checkpoint " + ch.zone);
                return;
            }
        }
        Debug.LogError("CheckPoint '" + checkPointNumber + "' not found");
    }
}