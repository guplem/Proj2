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
            State.SetState(new DeadState(10, Vector2.up), GameManager.Instance.playerManager);
        }
        else
        {
            Debug.LogError("Not in play mode.");
        }
    }

    [MenuItem("Bunker Bound/Cheats/Force reset until last checkpoint")]
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