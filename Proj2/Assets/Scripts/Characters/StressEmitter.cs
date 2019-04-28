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
    [Range(0f, 1f)] public float timeBetweenEmisions;

    [HideInInspector] private float effectRadius;
    [HideInInspector] private Vector3 emittingPoint;

    private IEnumerator coroutineHolder;

    public void Start()
    {
        this.emittingPoint = gameObject.transform.position;
        this.effectRadius = GetComponent<CircleCollider2D>().radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null && emitStress)
        {
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);

            coroutineHolder = AddStressOverTime(timeBetweenEmisions, stressAmountPerSecond);

            StartCoroutine(coroutineHolder);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);

            coroutineHolder = null;
        }
    }

    private IEnumerator AddStressOverTime(float timeBetweenEmisions, float stressAmountPerSecond)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEmisions);

            EmitStress(stressAmountPerSecond * timeBetweenEmisions);
        }
    }

    public static void EmitStress(float amount)
    {
        GameManager.Instance.playerManager.stressController.AddStress(amount);
    }

    private void OnDrawGizmos()
    {
        this.emittingPoint = gameObject.transform.position;

        Gizmos.DrawWireSphere(emittingPoint, effectRadius);
    }
}