using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class StressController : MonoBehaviour
{
    [Header("Stress Settings")]
    [Tooltip("Time untill stress starts to get removed.")] public float stressRemovalDelay;
    public float stressRemovalPerSecond;
    public float removeStressEveryXTime;

    private float _currentStress;

    public float currentStress
    {
        get
        {
            return _currentStress;
        }
        private set
        {
            if (value > 0 && value >= _currentStress)
            {
                StopCoroutine(stressRemovalCoroutine);
                stressRemovalCoroutine = RemoveStressAfterTime(stressRemovalDelay, removeStressEveryXTime);
                StartCoroutine(stressRemovalCoroutine);
            }

            if (value < 0)
            {
                value = 0;
                StopCoroutine(stressRemovalCoroutine);
            }

            if (value > stressThreshold)
            {
                value = stressThreshold;
            }

            _currentStress = value;

            if (isPlayer)
            {
                GUIManager.Instance.BackgroundVignette.SetObjectActive(true);
                GUIManager.Instance.BackgroundVignette.SetOpacitySmooth(value / stressThreshold);
            }
        }
    }

    [SerializeField] private float stressThreshold;

    [SerializeField] private AudioSource audioPlayingWhileStressing;
    [SerializeField] private AudioSource audioPlayingWhileBeingChased;
    [SerializeField] private float timeToStopChasingAudio;


    private bool isPlayer;

    private IEnumerator stressRemovalCoroutine;

    private void Start()
    {
        isPlayer = GetComponent<PlayerManager>() != null;
        stressRemovalCoroutine = RemoveStressAfterTime(stressRemovalDelay, removeStressEveryXTime);
    }

    public bool AddStress(float amount)
    {
        SetStress(currentStress + amount);

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

        StartCoroutine(Utils.LowerVolumeAndStopSounds(audioPlayingWhileStressing));
    }

    private void SetStress(float qty)
    {
        currentStress = qty;
    }

    public void IsBeinChased()
    {
        CancelInvoke();

        if (!audioPlayingWhileBeingChased.isPlaying)
            audioPlayingWhileBeingChased.Play();

        StartCoroutine(Utils.LowerVolumeAndStopSounds(audioPlayingWhileStressing));

        Invoke("StopBeingChased", timeToStopChasingAudio);
    }

    public void StopBeingChased()
    {
        StartCoroutine(Utils.LowerVolumeAndStopSounds(audioPlayingWhileBeingChased));

        if (currentStress > 0)
            if (!audioPlayingWhileStressing.isPlaying)
                audioPlayingWhileStressing.Play();
    }
}
