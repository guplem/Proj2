using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class SetCheckPointCheat : EditorWindow
{
    [MenuItem("Bunker Bound/Cheats/Set checkpoint")]
    public static void ShowWindow()
    {
        GetWindow<SetCheckPointCheat>("Checkpoint cheats");
    }

    string checkPointNumberString = "-1";

    private void OnGUI()
    {
        GUILayout.Label("Write the checkpoint number: ", EditorStyles.boldLabel);

        checkPointNumberString = EditorGUILayout.TextField("Zone number", checkPointNumberString);

        if (GUILayout.Button("Set new CheckPoint"))
        {
            int checkPointNumber = 0;

            if (int.TryParse(checkPointNumberString, out checkPointNumber))
            {
                if (!Application.isPlaying)
                {
                    Debug.LogError("The game is not in play mode.");
                    return;
                }

                CheckPoint[] CheckPoints = (CheckPoint[])FindObjectsOfType(typeof(CheckPoint));
                foreach (CheckPoint ch in CheckPoints)
                {
                    if (ch.zone == checkPointNumber)
                    {
                        GameManager.Instance.CheckPointReached(ch);
                        Debug.Log("Forced checkpoint " + ch.zone);
                        return;
                    }
                }
                Debug.LogError("CheckPoint '" + checkPointNumber + "' not found");


            }
            else
            {
                Debug.LogError("The zone is not a numebr");
            }

        }

        if (GUILayout.Button("Hit player"))
        {
            Cheats.HitPlayer();
        }

        if (GUILayout.Button("Reset until last checkpoint"))
        {
            Cheats.ResetTilLastCheckpoint();
        }
    }
}
