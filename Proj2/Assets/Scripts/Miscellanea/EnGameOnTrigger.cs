using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnGameOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>())
        {
            Debug.Log("End game");
            Application.Quit();
        }
    }
}
