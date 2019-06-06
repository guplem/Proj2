using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeCheats : MonoBehaviour
{

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
