using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager
{
    private GameObject gameObject;
    private List<AudioSource> audioSources;

    public AudioManager(GameObject objectManaged)
    {
        this.gameObject = objectManaged;
        audioSources = new List<AudioSource>();
    }

    public void PlaySound(Sound sound, bool loop)
    {
        AudioSource audioSource  = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        //TODO: working with audioSource

        //TODO: Destroy audiosource at the end
    }



}
