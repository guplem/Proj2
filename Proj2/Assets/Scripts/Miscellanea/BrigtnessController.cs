using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrigtnessController : MonoBehaviour
{
    /*[SerializeField] private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    private void Start()
    {
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
    }

    private void Update()
    {
        FloatParameter f = new FloatParameter();
        f.value = colorGrading.postExposure.value + 0.1f;
        colorGrading.postExposure = f;


    }*/

    private SpriteRenderer rnd;
    private float defaultAlpha;

    private void Start()
    {
        rnd = GetComponent<SpriteRenderer>();
        defaultAlpha = rnd.color.a;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                SetAlpha(rnd.color.a + 0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                SetAlpha(rnd.color.a - 0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                SetAlpha(defaultAlpha);
            }
        }
    }

    private void SetAlpha(float a)
    {
        rnd.color = new Color(rnd.color.r, rnd.color.g, rnd.color.b, a);
    }
}
