using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : Interactable
{
    // [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask interactablesLayer;

    public override void OnStartInteract(CharacterManager interactingCharacter)
    {
        interactingCharacter.SetState(new PushPullState(interactingCharacter));
        transform.SetParent(interactingCharacter.transform);
        GetComponent<SpriteRenderer>().color = Color.red;

        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        // Physics2D.IgnoreLayerCollision(playerLayer, interactablesLayer, true);
    }

    public override void OnEndInteract(CharacterManager interactingCharacter)
    {
        transform.SetParent(null);
        GetComponent<SpriteRenderer>().color = Color.blue;
        interactingCharacter.behaviourTree.SetNextState(true);

        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        // Physics2D.IgnoreLayerCollision(playerLayer, interactablesLayer, false);
    }

}
