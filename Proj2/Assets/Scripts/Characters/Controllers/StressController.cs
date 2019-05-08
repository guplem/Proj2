using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class StressController : MonoBehaviour
{
    public float stressRemovalDelay;
    public float stressRemovalPerSecond;
    public float removeStressEveryXTime;

    private float currentStress;
    [SerializeField] private float stressThreshold;
    private bool isPlayer;


    private IEnumerator stressRemoval;

    private void Start()
    {
        isPlayer = GetComponent<PlayerManager>() != null;

    }

    public bool AddStress(float amount)
    {
        SetStress(currentStress + amount);

        if (stressRemoval != null)
        {
            StopCoroutine(stressRemoval);
            stressRemoval = null;
        }
        stressRemoval = RemoveStressAfterTime(stressRemovalDelay, removeStressEveryXTime);
        StartCoroutine(stressRemoval);

        if (currentStress >= stressThreshold)
            ActionOnStressThreshold();

        return true;
    }

    public void ActionOnStressThreshold()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.
        //playerManager.behaviourTree.SetStressed();

        // SetStress(stressThreshold);
    }

    public IEnumerator RemoveStressAfterTime(float timeToStart, float timeBetweenDecrease)
    {
        yield return new WaitForSeconds(timeToStart);

        while (currentStress > 0)
        {
            yield return new WaitForSeconds(timeBetweenDecrease);
            SetStress(currentStress - (stressRemovalPerSecond * timeBetweenDecrease));
        }
        StopCoroutine(stressRemoval);
        stressRemoval = null;
    }

    private void SetStress(float qty)
    {
        if (qty > stressThreshold)
        {
            currentStress = stressThreshold;
        }
        else
        {
            currentStress = qty;
        }

        if (currentStress < 0)
            currentStress = 0;

        Debug.Log("Current stress: " + currentStress);
        if (isPlayer)
            GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(currentStress / stressThreshold);
    }
}
