using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Properties", menuName = "CharacterProperties")]
public class CharacterProperties : ScriptableObject
{
    public float jumpForce;
    public Vector2 acceleration;
    public Vector2 maxWalkVelocity;
    public Vector2 maxRunVelocity;
    public Vector2 maxCrouchVelocity;
    public Vector2 maxOnAirVelocity;

    //Default values
    public CharacterProperties()
    {
        this.jumpForce = 2;
        this.acceleration = new Vector2(0.25f, 0.25f);
        this.maxWalkVelocity = new Vector2(4f, 4f);
        this.maxRunVelocity = new Vector2(5f, 5f);
        this.maxCrouchVelocity = new Vector2(5f, 20f);
        this.maxOnAirVelocity = new Vector2(5f, 50f);
    }
}