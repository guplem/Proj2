using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialPhysics", menuName = "MaterialPhysics")]
public class MaterialPhysics : ScriptableObject
{
    [Range(0f, 1f)]
    [SerializeField] public float friction;
    [Range(0f, 1f)]
    [SerializeField] public float bounciness;
    [Range(0f, 1f)]
    [SerializeField] public float Hardnesh;
}
