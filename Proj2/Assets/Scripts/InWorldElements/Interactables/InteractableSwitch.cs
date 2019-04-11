using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class InteractableSwitch : Interactable
{
    
    protected override void OnStartInteract(CharacterManager interactingCharacter)
    {
        SwitchAllActivables(interactingCharacter);
    }

    protected override void OnEndInteract(CharacterManager interactingCharacter)
    {
        //Nothing happens
    }


}