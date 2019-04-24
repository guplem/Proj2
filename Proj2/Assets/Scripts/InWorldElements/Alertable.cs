using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterManager))]
public class Alertable : MonoBehaviour
{

    public Vector2 alertPosition { get; private set; }

    public static Alertable debugger;
    private void Awake()
    {
        if (debugger == null)
        {
            debugger = this;
        }
    }

    public static void AlertAllInRadius(Vector2 position, float radius)
    {
        if (radius <= 0) return;

        if (debugger != null) //Check if there is any object with an Alertable component set as debugger 
            debugger.StartCoroutine(debugger.DebugAlertRadius(position, radius));

        //TODO --> use "Alert(...)"
    }

    public static void AlertAllWithVisualContact(Vector2 position)
    {
        //TODO --> use "Alert(...)"
    }

    public static void AlertAll(Vector2 position, List<Alertable> toAlert)
    {
        foreach (Alertable alertable in toAlert)
            alertable.Alert(position);
    }

    public void Alert(Vector2 position)
    {
        alertPosition = position;
        
        //TODO --> Alert the brain in some way? (change the behaviour tree to an alrt one if position != null ???)
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
            //if (alertRadiusZones[position] < radiusOfAlert)
            //{
                alertRadiusZones.Remove(position);
                alertRadiusZones.Add(position, radiusOfAlert);
            //}
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