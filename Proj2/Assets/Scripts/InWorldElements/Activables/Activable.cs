﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{

    [SerializeField] public bool defaultState;
    public bool currentState { get; private set; }

    [SerializeField] public bool singleActivation;
    [HideInInspector] public bool alreadyActivated;

    private void Awake()
    {
        currentState = defaultState;

        SetState(currentState, null);

        alreadyActivated = false;
    }

    public void SwitchState(CharacterManager characterActivating)
    {
        if (RegisterAndAskForActivation())
        {
            currentState = !currentState;
            SetState(currentState, characterActivating);
        }
    }

    protected abstract void SetState(bool state, CharacterManager characterActivating);

    //Returns true if the action can be done and registers the action
    private bool RegisterAndAskForActivation()
    {
        bool toReturn = ((singleActivation && !alreadyActivated) || !singleActivation);

        alreadyActivated = true;

        return toReturn;
    }


}
