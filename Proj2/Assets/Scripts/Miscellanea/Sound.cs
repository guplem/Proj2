﻿using System;
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

    [Range(-3, 3)]
    [Tooltip("This value will be modified in a collision event")]
    [SerializeField] public float pitch;

    [Range(-3, 3)]
    [SerializeField] public float pitchRandomization;

    public float randomizedPitch
    {
        get
        {
            float rp = Mathf.Clamp(pitch + UnityEngine.Random.Range(-pitchRandomization, pitchRandomization), 0.01f, 0.65f);
            return rp;
        }
        set { }
    }



    public Sound()
    {
        this.clip = null;
        this.audioMixerGroup = null;
        this.volume = 1;
        this.pitch = 0.2f;
        this.pitchRandomization = 0.1f;
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