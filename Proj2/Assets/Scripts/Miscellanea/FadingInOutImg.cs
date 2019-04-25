using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FadingInOutImg : MonoBehaviour
{

    [SerializeField] private AnimationCurve fade = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float fadeDuration = 1.5f;
    private float currentFadeTime = 0;
    [Range(-1, 1)]
    [SerializeField] private int fadeDirectionAtStart = 1;

    [Range(0f, 1f)]
    [SerializeField] private float fadeTimeAtStart = 0;

    private int fadeDirection { get => fadeDirectionChecked; set { if (value == 1 || value == 0 || value == -1) fadeDirectionChecked = value; } }
    private int fadeDirectionChecked;

    private List<Image> imagesToFade = new List<Image>();

    private void Start()
    {
        //imagesToFade = GetComponents<Image>().ToList();

        foreach (Transform child in transform)
        {
            List<Image> imagesToAdd = GetComponents<Image>().ToList();

            if (imagesToAdd.Count != 0)
                imagesToFade.AddRange(imagesToAdd);
        }

        //imagesToFade.AddRange(GetComponents<Image>().ToList());

        fadeDirection = fadeDirectionAtStart;
        currentFadeTime = fadeTimeAtStart;
        SetOpacityTo(GetNewOpacity());
    }


    public void SetObjectActive(bool enabled)
    {
        if (enabled)
        {
            gameObject.SetActive(true);
            fadeDirection = 1;
        }
        else
        {
            fadeDirection = -1;
            //Wil be disbled at the end of the fade
        }

        gameObject.SetActive(enabled);
    }


    private void Update()
    {
        //Is fading
        if (fadeDirection != 0)
        {
            currentFadeTime += Time.unscaledDeltaTime* fadeDirection;

            float newOpacity = GetNewOpacity();

            SetOpacityTo(newOpacity);

            if (newOpacity == 0 || newOpacity == 1)
            {
                fadeDirection = 0;
            }

            if (newOpacity == 0)
            {
                SetObjectActive(false);
            }
        }
        else
        {
            currentFadeTime = 0;
        }
    }

    private float GetNewOpacity()
    {
        return fade.Evaluate(currentFadeTime / fadeDuration);
    }

    private void SetOpacityTo(float opacity)
    {
        foreach (Image image in imagesToFade)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        }

    }

}
