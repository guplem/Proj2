using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class MaterialWithSound : MonoBehaviour
{

    [SerializeField] public MaterialPhysics materialPhysics;
    [SerializeField] public Sound sound;
    [SerializeField] public float minVelocityAtCollisionForMaxSoundVolume;

    private void Awake()
    {
        PhysicsMaterial2D pMat = new PhysicsMaterial2D();
        pMat.friction = materialPhysics.friction;
        pMat.bounciness = materialPhysics.bounciness;

        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = pMat;
        }
    }

    public MaterialWithSound()
    {
        this.materialPhysics = new MaterialPhysics();
        this.sound = null;
        this.minVelocityAtCollisionForMaxSoundVolume = 2f;
    }
}