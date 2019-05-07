using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[DisallowMultipleComponent]
public class StressEmitter : MonoBehaviour
{
    [Header("Stress Config")]
    public bool emitStress;
    public float stressAmountPerSecond;
    public float timeBetweenEmisions;

    [HideInInspector] private float effectRadius;
    [HideInInspector] private Vector3 emittingPoint;

    private IEnumerator coroutineHolder;
    private List<StressController> stressing;

    public void Start()
    {
        this.emittingPoint = gameObject.transform.position;
        this.effectRadius = GetComponent<CircleCollider2D>().radius;
        stressing = new List<StressController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StressController stressController = collision.GetComponent<StressController>();
        if (stressController != null && emitStress)
        {
            stressing.Add(stressController);
            //TODO check if there is a stressController duplicated.
            /*if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);*/

            if (coroutineHolder == null)
            {
                coroutineHolder = AddStressOverTime(timeBetweenEmisions, stressAmountPerSecond);
                StartCoroutine(coroutineHolder);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StressController stressController = collision.GetComponent<StressController>();
        if (stressController != null)
        {
            try
            {
                stressing.Remove(stressController);
                Debug.Log("Removing!");
            }
            catch (System.NullReferenceException) { }

            if (coroutineHolder != null && stressing.Count <= 0)
            {
                StopCoroutine(coroutineHolder);
                coroutineHolder = null;
                Debug.Log("Stopping Emitting");
            }
        }
    }

    public IEnumerator AddStressOverTime(float timeBetweenEmisions, float stressAmountPerSecond)
    {
        do
        {
            Debug.Log("Adding Stress Over Time Called");
            yield return new WaitForSeconds(timeBetweenEmisions);

            EmitStress(stressAmountPerSecond * timeBetweenEmisions);
        }
        while (true);
    }

    public void EmitStress(float amount)
    {
        Debug.Log("Called emit stress!");
        foreach (StressController sc in stressing)
        {
            sc.AddStress(amount);
            Debug.Log("d...");
        }
    }

    private void OnDrawGizmos()
    {
        this.emittingPoint = gameObject.transform.position;

        Gizmos.DrawWireSphere(emittingPoint, effectRadius);
    }


}