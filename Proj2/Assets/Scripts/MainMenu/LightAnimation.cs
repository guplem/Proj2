using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    private Activable act;
    // Start is called before the first frame update
    void Start()
    {
        act = GetComponent<Activable>();
        StartCoroutine(AnimLight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AnimLight()
    {
        while (true)
        {
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            act.SwitchState(null);

            yield return new WaitForSeconds(Random.Range(7f, 18f));

        }
    }
}
