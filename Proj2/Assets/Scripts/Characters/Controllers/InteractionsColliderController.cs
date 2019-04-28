using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649
[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class InteractionsColliderController : MonoBehaviour
{

    [SerializeField] private CharacterManager characterManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null)
            characterManager.OnInterectableTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>() != null)
            characterManager.OnInterectableTriggerExit(collision);
    }

}