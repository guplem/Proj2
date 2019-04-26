using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fading : MonoBehaviour
{

    [SerializeField] private AnimationCurve fade = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float fadeTimeDuration = 0.5f;


    [Header("On/Off default values")]

    [Range(0f, 1f)]
    [SerializeField] private float opacityAtEnabled = 1;

    [Range(0f, 1f)]
    [SerializeField] private float opacityAtDisabled = 0;


    [Header("Start values")]

    [Range(0, 1)]
    [SerializeField] private float targetOpacityAtStart = 1;

    [Range(0f, 1f)]
    [SerializeField] private float opacityAtStart = 0;



    private List<Image> imagesToFade = new List<Image>();
    private List<TextMeshProUGUI> textsToFade = new List<TextMeshProUGUI>();

    private float currentOpacity;
    private float targetOpacity;

    private void Start()
    {
        DetectAllFadeElementsIn(gameObject);

        Configure(targetOpacityAtStart, opacityAtStart);
    }

    private void DetectAllFadeElementsIn(GameObject gObject)
    {
        if (gObject == null)
            return;

        imagesToFade.AddRange(gObject.GetComponents<Image>().ToList());
        textsToFade.AddRange(gObject.GetComponents<TextMeshProUGUI>().ToList());

        foreach (Transform child in gObject.transform)
        {
            if (child == null)
                continue;

            DetectAllFadeElementsIn(child.gameObject);
        }
    }


    private void Update()
    {
        if (currentOpacity != targetOpacity)
        {
            int directionDifference = (currentOpacity > targetOpacity) ? -1 : 1;

            float newOpacity = currentOpacity + ( (1/fadeTimeDuration) * Time.unscaledDeltaTime * directionDifference);

            if (directionDifference > 0) // Increasing
            {
                newOpacity = newOpacity > targetOpacity ? targetOpacity : newOpacity;
            }
            else // Decreasing
            {
                newOpacity = newOpacity < targetOpacity ? targetOpacity : newOpacity;
            }

            newOpacity = Mathf.Clamp(newOpacity, 0, 1); // Ensure correct value

            SetOpacityInstantly(newOpacity);
        }
        else
        {
            if (targetOpacity <= opacityAtDisabled)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SetObjectActive(bool enabled)
    {
        if (enabled)
        {
            gameObject.SetActive(true);
            Configure(opacityAtEnabled, currentOpacity);
        }
        else
        {
            Configure(opacityAtDisabled, currentOpacity);
        }
    }

    private void Configure(float targetOpacity, float currentOpacity)
    {
        this.targetOpacity = targetOpacity;
        this.currentOpacity = currentOpacity;

        SetOpacityInstantly(currentOpacity);
    }

    public void SetOpacityInstantly(float opacity)
    {
        currentOpacity = opacity;

        float curvedOpacity = fade.Evaluate(opacity);

        foreach (Image image in imagesToFade)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, curvedOpacity);
        }

        foreach (TextMeshProUGUI text in textsToFade)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, curvedOpacity);
        }
    }

    public void SetOpacitySmooth(float opacity)
    {
        Configure(opacity, currentOpacity);
    }

}