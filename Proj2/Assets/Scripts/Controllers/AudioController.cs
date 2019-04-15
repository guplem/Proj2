using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private List<AudioSource> audioSources;

    [SerializeField] private int defaultAudioSources;

    private void Awake()
    {
        this.audioSources = new List<AudioSource>();

        for (int i = 0; i < defaultAudioSources; i++)
        {
            
        }
    }
    

    public void PlaySound(Sound sound, bool loop)
    {
        //TODO: working with audioSource

        //TODO: Destroy audiosource at the end

        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                //Play sound
                return;
            }
        }

        AddAudioSource().clip(sound)
    }

    private AudioSource AddAudioSource()
    {
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(newAudioSource);
        return newAudioSource;
    }

    private AudioSource ConfigureAudioSource(AudioSource auioSource, Sound sound)
    {
        auioSource.clip = sound.clip;
        return auioSource;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play the sound depending on this object's material and the collison's object's material

        /*
             1. See if the collisioned object has a MaterialWithSound
                1.a (true) 
                    - Play this gameobject's sound at collisioned.hardness*volume of the sound
                    - Play the collisioned gameobject sound at this.hardness*volume of the sound
                1.b (false) 
                    - Play this gameobject's sound at 100% volume of the sound
         */
    }

}
