using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{

    void OnStartInteract(CharacterManager interactingCharacter);
    void OnEndInteract(CharacterManager interactingCharacter);

}
