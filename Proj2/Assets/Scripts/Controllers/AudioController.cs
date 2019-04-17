using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private int defaultAudioSourcesQty = 0;
    [SerializeField] private bool playMaterialSoundAtCollision = true;
    [HideInInspector] private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        foreach (AudioSource audioSource in GetComponents<AudioSource>())
            audioSources.Add(audioSource);

        for (int i = audioSources.Count; i < defaultAudioSourcesQty; i++)
            AddAudioSource();
    }
    

    public void PlaySound(Sound sound, bool loop)
    {
        if (sound == null)
            return;

        ConfigureAudioSource(GetFreeAudioSource(), sound, loop).Play();
    }

    
    public AudioSource GetFreeAudioSource()
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

        Debug.LogWarning("An audio source have been added in runtime for the object '" + gameObject.name + "'", gameObject);

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

        if (playMaterialSoundAtCollision)
        {
            MaterialWithSound mat = GetComponent<MaterialWithSound>();
            if (mat != null)
            {
                if (mat.sound != null)
                {
                    MaterialWithSound colMat = collision.gameObject.GetComponent<MaterialWithSound>();
                    if (colMat == null)
                    { 
                        colMat = collision.gameObject.AddComponent<MaterialWithSound>().SetDefaultValues();
                        Debug.LogWarning("'" + collision.gameObject.name + "' does not have a MaterialWithSound attatched. Attatching it dinamically");
                    }

                    PlaySound(Sound.GetSoundOfColision(mat, colMat, collision.relativeVelocity.magnitude), false);
                    PlaySound(Sound.GetSoundOfColision(colMat, mat, collision.relativeVelocity.magnitude), false);
                }
            }
        }

    }


}