using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour, Interactable
{

    [HideInInspector] public Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void OnEndInteract(CharacterManager interactingCharacter)
    {
        //Nothing
    }

    public void OnStartInteract(CharacterManager interactingCharacter)
    {
        if (interactingCharacter is PlayerManager)
        {
            PlayerManager player = (PlayerManager)interactingCharacter;

            player.inventory.StoreItem(this);

            gameObject.SetActive(false);
        }

    }

    public void Throw(Vector2 forceAndDirection, Vector2 throwPosition)
    {
        gameObject.SetActive(true);

        gameObject.transform.position = throwPosition;

        rb2d.AddForce(forceAndDirection, ForceMode2D.Impulse);
    }

    public void Drop(Vector2 dropPosition)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = dropPosition;
    }
}

