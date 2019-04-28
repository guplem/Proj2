using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class Sound : ScriptableObject, ICloneable
{

    [SerializeField] public AudioClip clip;

    [SerializeField] public AudioMixerGroup audioMixerGroup;

    [Tooltip("This value will be modified in a collision event")]
    [Range(0, 1)]
    [SerializeField] public float volume;

    [Range(0, 1)]
    [Tooltip("This value will be modified in a collision event")]
    [SerializeField] public float pitch;

    [Range(0, 1)]
    [SerializeField] public float pitchRandomization;

    public float randomizedPitch { get => Mathf.Clamp(pitch + UnityEngine.Random.Range(-pitchRandomization, -pitchRandomization), 0, 1); set { } }

    public Sound()
    {
        this.clip = null;
        this.audioMixerGroup = null;
        this.volume = 1;
        this.pitch = 0.5f;
        this.pitchRandomization = 0.2f;
    }

    public object Clone()
    {
        Sound newSound = ScriptableObject.CreateInstance<Sound>();

        newSound.clip = this.clip;
        newSound.volume = this.volume;
        newSound.audioMixerGroup = this.audioMixerGroup;
        newSound.pitch = this.pitch;
        newSound.pitchRandomization = this.pitchRandomization;

        return newSound;
    }



}