using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/MenuItem.html

public class Cheats : MonoBehaviour
{
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
}