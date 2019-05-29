using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ReseteableObject : MonoBehaviour
{

    [HideInInspector] private Vector3 initialPosition;
    [SerializeField] private int zone;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        this.initialPosition = transform.position;

        GameManager.Instance.ResetElementsUntilLastCheckPoint += ResetMethodToCallOnResetEvent;

        started = true;
    }

    /*public void Setup(int zone, Vector3 initialPosition)
    {
        this.initialPosition = initialPosition;
        this.zone = zone;
        GameManager.Instance.ResetElementsUntilLastCheckPoint += ResetMethodToCallOnResetEvent;
    }*/
    private void OnEnable()
    {
        if (!started)
            return;

        try
        {
            GameManager.Instance.ResetElementsUntilLastCheckPoint += ResetMethodToCallOnResetEvent;
        }
        catch (System.NullReferenceException)
        {

        }
        
    }

    private void OnDisable()
    {
        GameManager.Instance.ResetElementsUntilLastCheckPoint -= ResetMethodToCallOnResetEvent;
    }

    public void ResetMethodToCallOnResetEvent(int lastZoneVisited)
    {
        CharacterManager character = GetComponent<CharacterManager>();
        if (lastZoneVisited <= zone || character != null)
        {
            transform.position = initialPosition;

            
            if (character != null)
            {
                character.Configure();
            }
        }
    }

}
