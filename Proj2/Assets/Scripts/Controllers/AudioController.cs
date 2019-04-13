using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();
    }
    

    public void PlaySound(Sound sound, bool loop)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(audioSource);
        //TODO: working with audioSource

        //TODO: Destroy audiosource at the end
    }


}
