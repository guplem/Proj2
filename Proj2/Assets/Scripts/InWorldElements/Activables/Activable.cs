using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{

    [SerializeField] public bool defaultState;
    public bool currentState { get; private set; }

    [SerializeField] public bool singleActivation;
    [HideInInspector] public bool alreadyActivated;

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
    }

    public void SwitchState(CharacterManager characterActivating)
    {
        if (RegisterAndAskForActivation())
        {
            currentState = !currentState;
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
