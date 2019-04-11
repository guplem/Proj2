using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableBox : Activable
{
    // [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask interactablesLayer;

    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /*
    protected override void AtStartInteract(CharacterManager interactingCharacter)
    {
        interactingCharacter.SetState(new PushPullState(interactingCharacter));
        transform.SetParent(interactingCharacter.transform);
        rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void OnEndInteract(CharacterManager interactingCharacter)
    {
        transform.SetParent(null);
        interactingCharacter.behaviourTree.SetNextState(true);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
    */

    protected override void ForceSetState(bool state, CharacterManager characterActivating)
    {
        if (characterActivating != null)
            if (state == true)
            {
                transform.SetParent(characterActivating.transform);
                characterActivating.SetState(new PushPullState(characterActivating));
                rb2d.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                transform.SetParent(null);
                characterActivating.behaviourTree.CalculateAndSetNextState(true);
                rb2d.bodyType = RigidbodyType2D.Dynamic;
            }
    }
}
