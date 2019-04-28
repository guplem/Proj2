using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ReseteableObject : MonoBehaviour
{

    [HideInInspector] private Vector3 initialPosition;
    [HideInInspector] private int zone;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup(int zone, Vector3 initialPosition)
    {
        this.initialPosition = initialPosition;
        this.zone = zone;
        GameManager.Instance.ResetElementsUntilLastCheckPoint += ResetMethodToCallOnResetEvent;
    }

    private void OnDisable()
    {
        GameManager.Instance.ResetElementsUntilLastCheckPoint -= ResetMethodToCallOnResetEvent;
    }

    public void ResetMethodToCallOnResetEvent(int lastZoneVisited)
    {
        if (lastZoneVisited <= zone)
        {
            transform.position = initialPosition;

            CharacterManager character = GetComponent<CharacterManager>();
            if (character != null)
            {
                character.brain = character.defaultBrain;
                character.behaviourTree = character.defaultBehaviourTree;
            }
        }
    }

}
