using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioController))]
[DisallowMultipleComponent]
public class MaterialWithSound : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] public MaterialPhysics materialPhysics;
    [Header("Sound")]
    [SerializeField] private bool playSoundsAtColision = true;
    [SerializeField] public Sound sound;
    [Tooltip("Used to generate sound effects")]
    [SerializeField] public float minVelocityAtCollisionForMaxSoundVolume = 10f;
    [SerializeField] public float minVelocityAtCollisionForCollisionSound = 0.4f;
    [Range(0.001f, 0.65f)]
    [Tooltip("Used to generate sound effects")]
    [SerializeField] public float objectSize = 0.25f;
    [HideInInspector] private AudioController audioController;
    [SerializeField] private bool alertAlertablesOnCollision = false;

    private void Awake()
    {
        if (materialPhysics == null)
            SetDefaults();

        PhysicsMaterial2D pMat = new PhysicsMaterial2D();
        pMat.friction = materialPhysics.friction;
        pMat.bounciness = materialPhysics.bounciness;

        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = pMat;
        }

        audioController = GetComponent<AudioController>();
    }

    public MaterialWithSound SetDefaults()
    {
        this.materialPhysics = ScriptableObject.CreateInstance<MaterialPhysics>(); //new MaterialPhysics();
        this.sound = null;
        this.minVelocityAtCollisionForMaxSoundVolume = 10f;
        this.minVelocityAtCollisionForCollisionSound = 0.4f;

        return this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (this.playSoundsAtColision)
        {
            //MaterialWithSound mat = GetComponent<MaterialWithSound>();

            if (sound != null)
            {
                if (collision.relativeVelocity.magnitude < minVelocityAtCollisionForCollisionSound)
                    return;

                MaterialWithSound colMat = collision.gameObject.GetComponent<MaterialWithSound>();
                if (colMat == null)
                {
                    colMat = collision.gameObject.AddComponent<MaterialWithSound>(); //.SetDefaultValues();
                    Debug.LogWarning("'" + collision.gameObject.name + "' does not have a MaterialWithSound attatched. Attatching it dinamically", colMat.gameObject);
                }

                audioController.PlaySound(GetSoundOfColision(this, colMat, collision.relativeVelocity.magnitude), false, alertAlertablesOnCollision);
                //PlaySound(Sound.GetSoundOfColision(colMat, mat, collision.relativeVelocity.magnitude), false, true);
            }

        }

    }

    public static Sound GetSoundOfColision(MaterialWithSound materialOfMovingObject, MaterialWithSound collidedMaterial, float relativeVelocity)
    {
        if (materialOfMovingObject.sound == null)
            return null;

        //Construct the new sound
        Sound sound = (Sound)materialOfMovingObject.sound.Clone();


        //Modify the sound to get the proper sound from the collision

        sound.pitch = 1 - materialOfMovingObject.objectSize; //As bigger the object, lower is the pitch


        float volumeByCollision = (relativeVelocity * sound.volume) / materialOfMovingObject.minVelocityAtCollisionForMaxSoundVolume; //The faster the collision, louder is the sound
        sound.volume = volumeByCollision * collidedMaterial.materialPhysics.hardnesh; //The harder the collided object, louder is the sound


        return sound;
    }

}