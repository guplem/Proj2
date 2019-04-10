using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressManager
{
    private float currentStress;
    private float stressThreshold;

    private PlayerManager playerManager;

    public StressManager(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public bool AddStress(float amount)
    {
        currentStress += amount;
        if (currentStress >= stressThreshold)
            SomethingHappens();
        return true;
    }

    public void SomethingHappens()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.

    }
}
