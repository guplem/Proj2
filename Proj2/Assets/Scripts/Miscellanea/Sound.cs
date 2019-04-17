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

    [Range(0, 1)]
    [Tooltip("This value will be modified in a collision event")]
    [SerializeField] public float pitch;

    [Range(0, 1)]
    [SerializeField] public float pitchRandomization;

    public float randomizedPitch { get => Mathf.Clamp(pitch + UnityEngine.Random.Range(-pitchRandomization, -pitchRandomization), 0, 1); set { } }




    public Sound(AudioClip clip, float volume, AudioMixerGroup audioMixerGroup, float pitch, float pitchRandomization)
    {
        this.clip = clip;
        this.volume = volume;
        this.audioMixerGroup = audioMixerGroup;
        this.pitch = pitch;
        this.pitchRandomization = pitchRandomization;
    }

    public object Clone()
    {
        return new Sound(this.clip, this.volume, this.audioMixerGroup, this.pitch, this.pitchRandomization);
    }

    public static Sound GetSoundOfColision(MaterialWithSound originMat, MaterialWithSound colMat, float relativeVelocity)
    {
        if (originMat.sound == null)
            return null;

        //Construct the new sound
        Sound sound = (Sound)originMat.sound.Clone();



        //Modify the sound to get the proper sound from the collision

        sound.pitch = 1 - originMat.materialPhysics.size; //As bigger the object, lower is the pitch

        float volumeByCollision = (relativeVelocity * sound.volume) / originMat.minVelocityAtCollisionForMaxSoundVolume; //The faster the collision, louder is the sound
        sound.volume = volumeByCollision * colMat.materialPhysics.hardnesh; //The harder the collided object, louder is the sound



        return sound;
    }

}