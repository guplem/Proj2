using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0168 // Variable is declared but never used
public class InteractableSwitch : MonoBehaviour, Interactable
{
    [SerializeField] private bool AreLightsOn;
    [SerializeField] private InteractableLight[] interactableLights;

    private void Start()
    {
        SwitchAllLightsTo(AreLightsOn);
    }

    public void OnStartInteract()
    {
        Debug.Log("Begin interact");

        AreLightsOn = !AreLightsOn;

        SwitchAllLightsTo(AreLightsOn);
    }

    public void OnEndInteract()
    {
        //Nothing happens
        Debug.Log("End interact");
    }

    public void SwitchAllLightsTo(bool state)
    {
        foreach (InteractableLight light in interactableLights)
        {
            light.Switch(AreLightsOn);
        }
    }

}