using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class MaterialWithSound : MonoBehaviour
{

    [SerializeField] public MaterialPhysics materialPhysics;
    [SerializeField] public Sound sound;
    [Tooltip("Used to generate sound effects")]
    [SerializeField] public float minVelocityAtCollisionForMaxSoundVolume = 10;
    [Range(0f, 1f)]
    [Tooltip("Used to generate sound effects")]
    [SerializeField] public float objectSize = 0.5f;

    private void Awake()
    {
        if (materialPhysics == null)
            SetDefaultValues();

        PhysicsMaterial2D pMat = new PhysicsMaterial2D();
        pMat.friction = materialPhysics.friction;
        pMat.bounciness = materialPhysics.bounciness;

        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = pMat;
        }
    }

    public MaterialWithSound SetDefaultValues()
    {
        this.materialPhysics = ScriptableObject.CreateInstance<MaterialPhysics>(); //new MaterialPhysics();
        this.sound = null;
        this.minVelocityAtCollisionForMaxSoundVolume = 2f;

        return this;
    }
}