using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressEmitter
{
    private Vector3 emittingPoint;
    private float radius;

    public StressEmitter(Vector3 emittingPoint)
    {
        this.emittingPoint = emittingPoint;
    }

    public void EmitStressBurst(int amount)
    {
        StressManager[] temp = SearchForReceivers();
        if (temp.Length < 1)
            return;
        foreach (StressManager stressManager in temp)
        {
            stressManager.AddStress(amount);
        }

    }

    public void AddStressOverTime(int amount, float cooldown)
    {
        StressManager[] temp = SearchForReceivers();
        if (temp.Length < 1)
            return;
        StartCoroutine(AddStressToTargetsOverTime(temp, amount, cooldown));

    }

    public StressManager[] SearchForReceivers()
    {
        StressManager[] stressManagerArray;

        Collider2D[] colArray = Physics2D.OverlapCircle(emittingPoint, radius, GameManager.Instance.playerLayer);

        if (colArray.Length < 1)
            return null;

        stressManagerArray = new StressManager[colArray.Length];

        for (int i = 0; i < colArray.Length - 1; i++)
        {
            try
            {
                stressManagerArray[i] = colArray[i].GetComponent<PlayerManager>().stressManager;
            }
            catch
            {
                Debug.Log("Doesn't have StressManager");
            }
        }
        return stressManagerArray;
    }

    IEnumerator AddStressToTargetsOverTime(StressManager[] targets, int amount, float cooldown)
    {
        foreach (StressManager target in targets)
        {
            target.AddStress();
        }
        
    }
}
