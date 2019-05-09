using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class StressController : MonoBehaviour
{
    public float stressPassiveRemovalDelay;
    public float stressRemovalPerSecond;

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
        stressRemoval = RemoveStressAfterTime(stressPassiveRemovalDelay, 0.1f);
        StartCoroutine(stressRemoval);

        if (currentStress >= stressThreshold)
            ActionOnStressThreshold();

        return true;
    }

    public void ActionOnStressThreshold()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.
        //playerManager.behaviourTree.SetStressed();

        SetStress(stressThreshold);
    }

    public IEnumerator RemoveStressAfterTime(float timeToStart, float timeBetweenDecrease)
    {
        yield return new WaitForSeconds(timeToStart);

        while (currentStress > 0)
        {
            yield return new WaitForSeconds(timeBetweenDecrease);
            currentStress -= stressRemovalPerSecond * Time.deltaTime;

            SetStress(currentStress - (stressRemovalPerSecond * timeBetweenDecrease));
        }
        StopCoroutine(stressRemoval);
        stressRemoval = null;
    }

    private void SetStress(float qty)
    {
        currentStress = qty;

        if (isPlayer)
            GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(currentStress / stressThreshold);
    }
}
