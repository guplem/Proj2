using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(AudioController))]
#pragma warning disable 0649
public abstract class Activable : MonoBehaviour
{

    [SerializeField] public bool defaultState;
    public bool currentState { get; private set; }

    [SerializeField] public bool singleActivation;
    [HideInInspector] public bool alreadyActivated;
    [SerializeField] private Sound onSound, offSound;
    private AudioController audioController;

    public enum ActivationType
    {
        Pickable,
        Movable,
        Other
    }

    public abstract ActivationType GetActivationType();

    private void Awake()
    {
        currentState = defaultState;

        SetState(currentState, null, false);

        alreadyActivated = false;

        audioController = GetComponent<AudioController>();
        if (audioController == null && (onSound != null || offSound != null))
            Debug.LogWarning("No audio controller found for " + gameObject.name, gameObject);
    }

    public void SwitchState(CharacterManager characterActivating)
    {
        if (RegisterAndAskForActivation())
        {
            currentState = !currentState;

            if (currentState)
            {
                if (audioController != null)
                    audioController.PlaySound(onSound, false, false);
            }
            else
            {
                if (audioController != null)
                    audioController.PlaySound(offSound, false, false);
            }

            SetState(currentState, characterActivating, true);
        }
    }

    protected abstract void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate);

    //Returns true if the action can be done and registers the action
    private bool RegisterAndAskForActivation()
    {
        bool toReturn = ((singleActivation && !alreadyActivated) || !singleActivation);

        alreadyActivated = true;

        return toReturn;
    }


}
