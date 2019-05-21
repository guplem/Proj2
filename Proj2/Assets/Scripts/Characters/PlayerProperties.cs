using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProperties", menuName = "Models/Properties/PlayerProperties",order = 1)]

public class PlayerProperties : CharacterProperties
{

    [Header("Materials")]
    public PhysicsMaterial2D DefaultMaterial;
    public PhysicsMaterial2D OnAirMaterial;
    public PlayerProperties()
    {
        Debug.Log("Starting player properties!");

    }
}
 
