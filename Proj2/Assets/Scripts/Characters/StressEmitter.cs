using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[DisallowMultipleComponent]
public class StressEmitter : MonoBehaviour
{
    [Header("Stress Config")]
    [SerializeField] private bool emitStress;
    [SerializeField] private float stressAmountPerSecond;
    [SerializeField] private float timeBetweenEmisions;

    private IEnumerator coroutineHolder;
    private List<StressController> stressing;

    public void Start()
    {
        stressing = new List<StressController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StressController stressController = collision.GetComponent<StressController>();
        if (stressController != null && emitStress)
        {
            if (!stressing.Contains(stressController))
                stressing.Add(stressController);

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
            }
            catch (System.NullReferenceException) { }

            if (coroutineHolder != null && stressing.Count <= 0)
            {
                StopCoroutine(coroutineHolder);
                coroutineHolder = null;
            }
        }
    }

    public IEnumerator AddStressOverTime(float timeBetweenEmisions, float stressAmountPerSecond)
    {
        do
        {
            yield return new WaitForSeconds(timeBetweenEmisions);

            EmitStress(stressAmountPerSecond * timeBetweenEmisions);
        }
        while (true);
    }

    public void EmitStress(float amount)
    {
        foreach (StressController sc in stressing)
        {
            sc.AddStress(amount);
        }
    }


}