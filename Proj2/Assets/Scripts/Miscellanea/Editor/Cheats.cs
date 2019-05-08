using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [MenuItem("Bunker Bound/Cheats/Hit Player")]
    public static void HitPlayer()
    {
        if (Application.isPlaying)
        {
            GameManager.Instance.HitPlayer();
        }
        else
        {
            Debug.LogError("Not in play mode.");
        }
    }

    [MenuItem("Bunker Bound/Cheats/Reset until last checkpoint")]
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