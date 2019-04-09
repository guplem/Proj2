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
        ForceSetState(defaultState);
        alreadyActivated = false;
    }

    public void SetState(bool state)
    {
        if (RegisterAndAskForActivation())
        {
            ForceSetState(state);
            Debug.Log("Activation called");
        }
    }

    protected abstract void ForceSetState(bool state);

    //Returns true if the action can be done and registers the action
    private bool RegisterAndAskForActivation()
    {
        bool toReturn = ((singleActivation && !alreadyActivated) || !singleActivation);

        alreadyActivated = true;

        return toReturn;
    }


}
