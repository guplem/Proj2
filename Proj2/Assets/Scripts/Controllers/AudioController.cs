using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
#pragma warning disable CS0649
public class AudioController : MonoBehaviour
{
    [SerializeField] private int defaultAudioSourcesQty;
    [HideInInspector] private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();

        foreach (AudioSource audioSource in GetComponents<AudioSource>())
            audioSources.Add(audioSource);

        for (int i = audioSources.Count; i < defaultAudioSourcesQty; i++)
            AddAudioSource();

    }


    public void PlaySound(Sound sound, bool loop, bool sendAlert)
    {
        if (sound == null)
            return;

        ConfigureAudioSource(GetFreeAudioSource(), sound, loop).Play();

        if (sendAlert)
            SendAlertsBySound(sound);
    }


    private void SendAlertsBySound(Sound sound)
    {
        float radiusOfAlertAtMaxVolume = 23;
        float radiusOfAlert = sound.volume * radiusOfAlertAtMaxVolume;

        Alertable.AlertAllInRadius(transform.position, radiusOfAlert);
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return AddAudioSource();
    }

    private AudioSource AddAudioSource()
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;
        newAudioSource.spatialBlend = 1f;
        newAudioSource.rolloffMode = AudioRolloffMode.Linear;
        newAudioSource.minDistance = 4.0f;
        newAudioSource.maxDistance = 30.0f;
        audioSources.Add(newAudioSource);

        //Fdilter debug messages
        if (defaultAudioSourcesQty + 5 == audioSources.Count)
        {
            Debug.LogWarning("More than 5 audio sources have been added in RUNTIME for the object '" + gameObject.name + "'", gameObject);
        }
        if (defaultAudioSourcesQty + 10 == audioSources.Count)
        {
            Debug.LogWarning("More than 10 audio sources have been added in RUNTIME for the object '" + gameObject.name + "'", gameObject);
        }
        else if (defaultAudioSourcesQty == 0 && audioSources.Count == 1)
        {
            Debug.LogWarning("An audio source have been added in RUNTIME for the object '" + gameObject.name + "' while it has been configured to do not have any by default.", gameObject);
        }
        //Debug.LogWarning("An audio source have been added in runtime for the object '" + gameObject.name + "'", gameObject);


        return newAudioSource;
    }

    private AudioSource ConfigureAudioSource(AudioSource audioSource, Sound sound, bool loop)
    {
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.outputAudioMixerGroup = sound.audioMixerGroup;
        audioSource.pitch = sound.randomizedPitch;

        audioSource.loop = loop;

        return audioSource;
    }

    public void StopAllSounds()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            StartCoroutine(Utils.LowerVolumeAndStopSounds(audioSource));
        }
    }

}