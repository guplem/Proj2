using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class charAnimFunctions : MonoBehaviour
{
    [SerializeField] private CharacterManager character;
    [SerializeField] private bool alertBySound;
    // Start is called before the first frame update

    public void PlayWalkSound()
    {
        character.PlayWalkSound(alertBySound);
    }
}
