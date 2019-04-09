using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class InteractableSwitch : Interactable
{
    [SerializeField] private Activable[] connectedActivables;

    protected override void AtStartInteract(CharacterManager interactingCharacter)
    {
        currentState = !currentState;
        SwitchAllActivablesTo(currentState);
    }

    public override void OnEndInteract(CharacterManager interactingCharacter)
    {
        //Nothing happens
    }

    private void SwitchAllActivablesTo(bool state)
    {
        foreach (Activable item in connectedActivables)
        {
            item.SetState(currentState);
        }
    }


}