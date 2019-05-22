using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableAlarm : MonoBehaviour
{
    [Header("Alarm Settings")]
    public bool alertsOnActivation;
    public float alarmSoundDuration;
    public float alertRadius;

    [Header("Light Settings")]
    public float lightFlickDelay;
    public float lightAlphaInterpolation;

    [Header("Sound Configuration")]
    public Sound alarmSound;

    private AudioController audioController;
    private Light2D.LightSprite lightSprite;
    [SerializeField] private GameObject lightEmmitter;

    private IEnumerator lightCoroutine;
    private IEnumerator alarmCoroutine;

    private void Start()
    {
        lightEmmitter.SetActive(false);
        audioController = GetComponent<AudioController>();
        if (alarmSoundDuration <= 0)
        {
            Debug.LogWarning("Duració de l'alarma incorrecta a " + gameObject.name, gameObject);
        }
        lightCoroutine = PlayLightAnimation();
        alarmCoroutine = StopAll();
    }

    public void ActivateAlarm()
    {
        lightEmmitter.SetActive(true);
        audioController.PlaySound(alarmSound, true, alertsOnActivation);
        if (alertsOnActivation)
            Alertable.AlertAllInRadius(new Vector2(transform.position.x, transform.position.y), alertRadius);
        StartCoroutine(alarmCoroutine);
        StartCoroutine(lightCoroutine);
    }

    public IEnumerator StopAll()
    {
        yield return new WaitForSeconds(alarmSoundDuration);
        audioController.StopAllSounds();
        lightEmmitter.SetActive(false);
        StopCoroutine(lightCoroutine);
        Debug.Log("Stopping Everything!");
        StopCoroutine(alarmCoroutine);
    }

    public IEnumerator PlayLightAnimation()
    {
        //Color colorsito = lightSprite.Color;
        while (true)
        {
            yield return new WaitForSeconds(lightFlickDelay);
            lightEmmitter.SetActive(!lightEmmitter.activeSelf);
            //colorsito.a = Mathf.Lerp(0, 1, lightAlphaInterpolation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (alertsOnActivation)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), alertRadius);
        }
    }


}
