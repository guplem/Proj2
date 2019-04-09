using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public bool singleInteraction;
    protected bool alreadyInteracted;

    [SerializeField] public bool defaultState;
    protected bool currentState;

    private void Awake()
    {
        currentState = defaultState;
        alreadyInteracted = false;
    }


    public void OnStartInteract(CharacterManager interactingCharacter)
    {
        if (RegisterAndAskForInteraction())
        {
            AtStartInteract(interactingCharacter);
        }
    }

    public abstract void OnEndInteract(CharacterManager interactingCharacter);


    //Returns true if the interaction can be done and registers the interaction
    private bool RegisterAndAskForInteraction()
    {
        bool toReturn = ((singleInteraction && !alreadyInteracted) || !singleInteraction);

        alreadyInteracted = true;

        return toReturn;
    }

    protected abstract void AtStartInteract(CharacterManager interactingCharacter);

}
