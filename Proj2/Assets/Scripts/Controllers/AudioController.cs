using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
#pragma warning disable CS0649
public class AudioController : MonoBehaviour
{
    [SerializeField] private int defaultAudioSourcesQty;
    [SerializeField] private bool playSoundsAtColision;
    [HideInInspector] private List<AudioSource> audioSources;

    private void Awake()
    {
        //playSoundsAtColision = false;
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
        float radiusAtMaxVolume = 10;
        float radiusOfAlert = sound.volume * radiusAtMaxVolume;

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

        audioSources.Add(newAudioSource);

        //Fdilter debug messages
        if (defaultAudioSourcesQty+5 == audioSources.Count)
        {
            Debug.LogWarning("More than 5 audio sources have been added in RUNTIME for the object '" + gameObject.name + "'. Maybe more AudioSources should be created by default during the scene's loading.", gameObject);
        }
        if (defaultAudioSourcesQty + 10 == audioSources.Count)
        {
            Debug.LogWarning("More than 10 audio sources have been added in RUNTIME for the object '" + gameObject.name + "'. Maybe more AudioSources should be created by default during the scene's loading.", gameObject);
        }
        else if (defaultAudioSourcesQty == 0 && audioSources.Count == 1)
        {
            Debug.LogWarning("An audio source have been added in RUNTIME for the object '" + gameObject.name + "' while it has been configured to do not have any by default.  Maybe some AudioSources should be created by default during the scene's loading.", gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (this.playSoundsAtColision)
        {
            MaterialWithSound mat = GetComponent<MaterialWithSound>();
            if (mat != null)
            {
                if (mat.sound != null)
                {
                    MaterialWithSound colMat = collision.gameObject.GetComponent<MaterialWithSound>();
                    if (colMat == null)
                    {
                        colMat = collision.gameObject.AddComponent<MaterialWithSound>(); //.SetDefaultValues();
                        Debug.LogWarning("'" + collision.gameObject.name + "' does not have a MaterialWithSound attatched. Attatching it dinamically");
                    }

                    PlaySound(Sound.GetSoundOfColision(mat, colMat, collision.relativeVelocity.magnitude), false, true);
                    PlaySound(Sound.GetSoundOfColision(colMat, mat, collision.relativeVelocity.magnitude), false, true);
                }
            }
        }

    }


}