using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractionsCollider : MonoBehaviour
{

    [SerializeField] private CharacterManager characterManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        characterManager.OnInterectableTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        characterManager.OnInterectableTriggerExit(collision);
    }


}