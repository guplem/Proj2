using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialPhysics", menuName = "MaterialPhysics")]
public class MaterialPhysics : ScriptableObject
{
    [Range(0f, 1f)]
    [Tooltip("Used to calculate physical behaviour")]
    [SerializeField] public float friction;
    [Range(0f, 1f)]
    [Tooltip("Used to calculate physical behaviour")]
    [SerializeField] public float bounciness;
    [Range(0f, 1f)]
    [Tooltip("Used to generate sound effects")]
    [SerializeField] public float hardness;
    
    public MaterialPhysics()
    {
        this.friction = 0.5f;
        this.bounciness = 0.25f;
        this.hardness = 1.0f;
    }
}