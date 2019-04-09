using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : MonoBehaviour, Interactable
{
    // [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask interactablesLayer;

    public void OnStartInteract(CharacterManager interactingCharacter)
    {
        interactingCharacter.SetState(new PushPullState(interactingCharacter));
        transform.SetParent(interactingCharacter.transform);
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().color = Color.red;

        // Physics2D.IgnoreLayerCollision(playerLayer, interactablesLayer, true);
    }

    public void OnEndInteract(CharacterManager interactingCharacter)
    {
        transform.SetParent(null);
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<SpriteRenderer>().color = Color.blue;
        interactingCharacter.behaviourTree.SetNextState(true);
        // Physics2D.IgnoreLayerCollision(playerLayer, interactablesLayer, false);
    }

}
