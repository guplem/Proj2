using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterManager))]
public class Alertable : MonoBehaviour
{

    public Vector2 alertPosition { get; private set; }
    private CharacterManager character;

    public static Alertable debugger;
    private void Awake()
    {
        if (debugger == null)
        {
            debugger = this;
        }
    }

    private void Start()
    {
        character = GetComponent<CharacterManager>();
    }

    public static void AlertAllInRadius(Vector2 position, float radius)
    {
        if (radius <= 0)
            return;

        if (debugger == null)
            return;

        debugger.StartCoroutine(debugger.DebugAlertRadius(position, radius));

        foreach (Collider2D objInRange in Physics2D.OverlapCircleAll(position, radius))
        {
            Alertable alertable = objInRange.GetComponent<Alertable>();
            if (alertable != null)
            {
                alertable.Alert(position);
            }
        }
        
    }

    public static void AlertAllWithVisualContact(Vector2 position)
    {
        Debug.LogError("'AlertAllWithVisualContact' is not implemented.");
    }

    public static void AlertAll(Vector2 position, List<Alertable> toAlert)
    {
        foreach (Alertable alertable in toAlert)
            alertable.Alert(position);
    }

    public void Alert(Vector2 position)
    {
        if (position == null)
            return;

        alertPosition = position;

        character.brain = new InvestigatingBrain(character, position);
    }


    


    //Only used by the debugger Alterable
    private Dictionary<Vector2, float> alertRadiusZones = new Dictionary<Vector2, float>();
    private IEnumerator DebugAlertRadius(Vector2 position, float radiusOfAlert)
    {
        try
        {
            alertRadiusZones.Add(position, radiusOfAlert);
        }
        catch (System.ArgumentException)
        {
            alertRadiusZones.Remove(position);
            alertRadiusZones.Add(position, radiusOfAlert);
        }

        // Set the time to wait until continuing the execution
        yield return new WaitForSeconds(1.0f);

        alertRadiusZones.Remove(position);
    }
    private void OnDrawGizmos()
    {
        foreach (KeyValuePair<Vector2, float> posAndRad in alertRadiusZones)
        {
            Gizmos.DrawWireSphere(posAndRad.Key, posAndRad.Value);
        }
    }

}