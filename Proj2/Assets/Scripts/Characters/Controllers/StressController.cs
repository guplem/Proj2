using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressController
{
    private float currentStress;
    private float stressThreshold;

    private PlayerManager playerManager;

    public StressController(PlayerManager playerManager, float stressThreshold)
    {
        this.playerManager = playerManager;
        this.stressThreshold = stressThreshold;
        this.currentStress = 0f;
    }

    public bool AddStress(float amount)
    {
        currentStress += amount;

        GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(currentStress / stressThreshold);

        if (currentStress >= stressThreshold)
            SomethingHappens();

        return true;
    }

    public void SomethingHappens()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.
        //playerManager.behaviourTree.SetStressed();
    }
}
