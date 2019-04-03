using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Properties", menuName = "CharacterProperties")]
public class CharacterProperties : ScriptableObject
{
    public float jumpForce;
    public float maxHorVelocity;
    public float maxVerVelocity;

}
