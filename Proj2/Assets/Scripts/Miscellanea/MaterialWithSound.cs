using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class MaterialWithSound : MonoBehaviour
{

    [SerializeField] private MaterialPhysics materialPhysics;
    [SerializeField] private Sound sound;

    private void Awake()
    {
        PhysicsMaterial2D mat = new PhysicsMaterial2D();
        mat.friction = materialPhysics.friction;
        mat.bounciness = materialPhysics.bounciness;

        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = mat;
        }
    }
}