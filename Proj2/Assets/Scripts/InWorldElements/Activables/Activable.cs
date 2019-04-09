using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{
    [SerializeField] public bool singleActivation;
    protected bool alreadyActivated;

    [SerializeField] public bool defaultState;
    protected bool currentState;


    private void Awake()
    {
        ForceSetState(defaultState, null);
        alreadyActivated = false;
    }

    public void SetState(bool state, CharacterManager characterActivating)
    {
        if (RegisterAndAskForActivation())
        {
            ForceSetState(state, characterActivating);
        }
    }

    protected abstract void ForceSetState(bool state, CharacterManager characterActivating);

    //Returns true if the action can be done and registers the action
    private bool RegisterAndAskForActivation()
    {
        bool toReturn = ((singleActivation && !alreadyActivated) || !singleActivation);

        alreadyActivated = true;

        return toReturn;
    }


}
