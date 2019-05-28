using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class StressController : MonoBehaviour
{
    public float stressRemovalDelay;
    public float stressRemovalPerSecond;
    public float removeStressEveryXTime;

    [SerializeField] private AudioSource audioPlayingWhileStressing;
    [SerializeField] private AudioSource audioPlayingWhileBeingChased;
    [SerializeField] private float timeToStopChasingAudio;

    private float currentStress;
    [SerializeField] private float maximumStress;
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

        //if (currentStress >= stressThreshold)
        //    ActionOnStressThreshold();

        if (currentStress > 0)
            if (!audioPlayingWhileBeingChased.isPlaying)
                if (!audioPlayingWhileStressing.isPlaying)
                    audioPlayingWhileStressing.Play();

        return true;
    }

    /*public void ActionOnStressThreshold()
    {
        //TODO idea is to make the player random movements, thingys or fuck-ups.
        //playerManager.behaviourTree.SetStressed();

        // SetStress(stressThreshold);
    }*/

    public IEnumerator RemoveStressAfterTime(float timeToStart, float timeBetweenDecrease)
    {
        yield return new WaitForSeconds(timeToStart);

        while (currentStress > 0)
        {
            yield return new WaitForSeconds(timeBetweenDecrease);
            SetStress(currentStress - (stressRemovalPerSecond * timeBetweenDecrease));
        }

        audioPlayingWhileStressing.Stop();
        StopCoroutine(stressRemoval);
        stressRemoval = null;
    }

    private void SetStress(float qty)
    {
        if (qty > maximumStress)
            currentStress = maximumStress;
        else
            currentStress = qty;

        if (currentStress < 0)
            currentStress = 0;

        if (isPlayer)
            GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(currentStress / maximumStress);
    }

    public void IsBeinChased()
    {
        CancelInvoke();

        if (!audioPlayingWhileBeingChased.isPlaying)
            audioPlayingWhileBeingChased.Play();
        audioPlayingWhileStressing.Stop();

        Invoke("StopBeingChased", timeToStopChasingAudio);
    }

    public void StopBeingChased()
    {
        audioPlayingWhileBeingChased.Stop();

        if (currentStress > 0)
            if (!audioPlayingWhileStressing.isPlaying)
                audioPlayingWhileStressing.Play();
    }
}
