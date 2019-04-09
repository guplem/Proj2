using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool singleActivation { get; set; }

    void OnStartInteract(CharacterManager interactingCharacter);
    void OnEndInteract(CharacterManager interactingCharacter);
}
