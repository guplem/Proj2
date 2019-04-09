using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
public class InteractableSwitch : MonoBehaviour, Interactable
{
    [SerializeField] private bool AreLightsOn;
    [SerializeField] private InteractableLight[] interactableLights;

    private void Start()
    {
        SwitchAllLightsTo(AreLightsOn);
    }

    public void OnStartInteract(CharacterManager interactingCharacter)
    {
        AreLightsOn = !AreLightsOn;

        SwitchAllLightsTo(AreLightsOn);
    }

    public void OnEndInteract(CharacterManager interactingCharacter)
    {
        //Nothing happens
    }

    public void SwitchAllLightsTo(bool state)
    {
        foreach (InteractableLight light in interactableLights)
        {
            light.Switch(AreLightsOn);
        }
    }

}