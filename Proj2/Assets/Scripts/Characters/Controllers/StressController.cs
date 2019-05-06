using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressController : MonoBehaviour
{
    private float currentStress;
    [SerializeField] private float stressThreshold;
    private bool isPlayer;

    private void Start()
    {
        isPlayer = GetComponent<PlayerManager>() != null;
    }

    public bool AddStress(float amount)
    {
        currentStress += amount;

        if (isPlayer)
            GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(currentStress / stressThreshold);

        if (currentStress >= stressThreshold)
            ActionOnStressThreshold();

        return true;
    }

    public void ActionOnStressThreshold()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.
        //playerManager.behaviourTree.SetStressed();
    }
}
