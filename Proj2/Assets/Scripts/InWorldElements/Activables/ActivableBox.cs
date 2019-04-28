using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActivableBox : Activable
{
    // [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask interactablesLayer;

    Rigidbody2D rb2d;

    protected override void SetState(bool state, CharacterManager characterActivating)
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

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

}
