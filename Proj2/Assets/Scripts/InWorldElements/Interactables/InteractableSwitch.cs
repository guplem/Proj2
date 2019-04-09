using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class InteractableSwitch : Interactable
{
    [SerializeField] private bool isEnabled;
    [SerializeField] private Activable[] connectedItems;

    private void Start()
    {
        /*if (connectedItems.Length != 0 && connectedItems != null)
            SwitchTo(isEnabled);*/
    }

    public override void OnStartInteract(CharacterManager interactingCharacter)
    {
        isEnabled = !isEnabled;
        SwitchTo(isEnabled);
    }

    public override void OnEndInteract(CharacterManager interactingCharacter)
    {
        //Nothing happens
    }

    public void SwitchTo(bool state)
    {
        foreach (Activable item in connectedItems)
        {
            item.Interact(isEnabled);
        }
    }

}