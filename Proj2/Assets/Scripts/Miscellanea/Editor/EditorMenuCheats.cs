using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMenuCheats : MonoBehaviour
{
    [MenuItem("Bunker Bound/Cheats/Hit Player #K")]
    public static void HitPlayer()
    {
        Cheats.HitPlayer();
    }

    [MenuItem("Bunker Bound/Cheats/Force reset until last checkpoint #R")]
    public static void ResetTilLastCheckpoint()
    {
        Cheats.ResetTilLastCheckpoint();
    }

    [MenuItem("Bunker Bound/Cheats/Toggle Selected Activable #T")]
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
}
