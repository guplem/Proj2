using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterManager))]
public class Alertable : MonoBehaviour
{
    [HideInInspector] public CharacterManager character;

    public static Alertable alertsManager;
    private void Awake()
    {
        if (alertsManager == null)
        {
            alertsManager = this;
        }
    }

    private void Start()
    {
        character = GetComponent<CharacterManager>();

        if (character == null)
        {
            Debug.LogWarning("'Alertable' implemented without a 'CharacterManager' component in the same GaemObject." + gameObject);
            Debug.Break();
        }
    }

    public static void AlertAllInRadius(Vector2 position, float radius)
    {
        if (radius <= 0)
            return;

        if (alertsManager == null) //Should never happen. alertsManager acts as a Singleton
            return;

        alertsManager.StartCoroutine(alertsManager.DebugAlertRadius(position, radius));

        foreach (Collider2D objInRange in Physics2D.OverlapCircleAll(position, radius))
        {
            Alertable alertable = objInRange.GetComponent<Alertable>();
            if (alertable != null)
            {
                if ((!(alertable.character.brain is ChasingBrain)) && (!(GameManager.Instance.playerManager.state is ReviveState)) )
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

        character.Alert(position);
    }




    /// DEBUGGING ///
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