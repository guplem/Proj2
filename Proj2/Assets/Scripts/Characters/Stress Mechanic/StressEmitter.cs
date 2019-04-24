using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class StressEmitter : MonoBehaviour
{

    /*[Header("Stress Config per Wave")]
    public float stressAmount;
    public float stressWaveDelay;*/

    [Header("Stress Config")]
    public bool emitStress;
    public float stressAmountPerSecond;
    [Range(0f, 1f)] public float stressPerSecondDelay;

    [Header("Others")]
    [SerializeField] private float radius;

    private Vector3 emittingPoint;
    [SerializeField] private bool stressingOut;

    private IEnumerator coroutineHolder;

    public void Start()
    {
        stressingOut = false;
        //GameManager.Instance.playerManager.GetStressManager().AddStress(amount);

        this.emittingPoint = gameObject.transform.position;
        this.radius = GetComponent<CircleCollider2D>().radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided with " + collision.transform.ToString());
        if (collision.GetComponent<PlayerManager>() != null && emitStress)
        {
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);
            stressingOut = true;
            coroutineHolder = AddStressOverTime(stressPerSecondDelay, stressAmountPerSecond);
            StartCoroutine(coroutineHolder);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerManager>() != null)
        {
            stressingOut = false;
            if (coroutineHolder != null)
                StopCoroutine(coroutineHolder);
            coroutineHolder = null;
        }
    }

    private void OnDrawGizmos()
    {
        this.emittingPoint = gameObject.transform.position;

        Gizmos.DrawWireSphere(emittingPoint, radius);
    }

    public static void AddStress(float amount)
    {
        GameManager.Instance.playerManager.GetStressManager().AddStress(amount);
    }

    /*private IEnumerator EmitStressWave(float delay)
    {
        while (stressingOut)
        {
            Debug.Log("Emitting Stress 2.0");
            AddStress(stressAmount);
            yield return new WaitForSeconds(delay);
        }
    }*/

    private IEnumerator AddStressOverTime(float delayBetweenStress, float amountPerSecond)
    {
        while (stressingOut)
        {
            yield return new WaitForSeconds(delayBetweenStress);
            Debug.Log("Getting Scared!");
            float amount = amountPerSecond * delayBetweenStress;
            AddStress(amount);
        }
    }
}
