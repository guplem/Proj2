using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public bool singleInteraction;
    [HideInInspector] public bool alreadyInteracted;

    [SerializeField] public bool interactAutomatically;

    [SerializeField] private Activable[] connectedActivables;

    private void Awake()
    {
        alreadyInteracted = false;
    }


    public void StartInteract(CharacterManager interactingCharacter)
    {
        if (RegisterAndAskForInteraction())
        {
            Debug.Log("'" + interactingCharacter.gameObject.name + "' is interacting with '" + gameObject.name + "'", gameObject);
            OnStartInteract(interactingCharacter);
        }
    }

    protected abstract void OnStartInteract(CharacterManager interactingCharacter);

    public void EndInteract(CharacterManager interactingCharacter)
    {
        OnEndInteract(interactingCharacter);
    }

    protected abstract void OnEndInteract(CharacterManager interactingCharacter);

    //Returns true if the interaction can be done and registers the interaction
    private bool RegisterAndAskForInteraction()
    {
        bool toReturn = ((singleInteraction && !alreadyInteracted) || !singleInteraction);

        alreadyInteracted = true;

        return toReturn;
    }

    protected void SwitchAllActivables(CharacterManager interactingCharacter)
    {
        foreach (Activable activable in connectedActivables)
        {
            activable.SwitchState(interactingCharacter);
        }
    }

}
