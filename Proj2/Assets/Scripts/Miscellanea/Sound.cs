using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
public class Sound : ScriptableObject
{
    public AudioClip sound;
    public float volume;
    public AudioMixerGroup audioMixerGroup;
    float pitchRandomization;
}
