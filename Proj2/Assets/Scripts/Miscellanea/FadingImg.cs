using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FadingImg : MonoBehaviour
{

    [SerializeField] private AnimationCurve fade = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float fadeTimeDuration = 1.5f;
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

        UpdateConfiguration(fadeDirectionAtStart, fadeTimeAtStart);
    }


    private void Update()
    {
        //Is fading
        if (fadeDirection != 0)
        {
            currentFadeTime += Time.unscaledDeltaTime* fadeDirection;
            currentFadeTime = Mathf.Clamp(currentFadeTime, 0, fadeTimeDuration);

            float newOpacity = GetCurrentOpacity();
            SetOpacityTo(newOpacity);

            if (newOpacity <= 0 && fadeDirection == -1)
            {
                gameObject.SetActive(false);
            }

            if (currentFadeTime <= 0 || currentFadeTime >= fadeTimeDuration)
            {
                fadeDirection = 0;
            }
        }
    }

    private float GetCurrentOpacity()
    {
        return fade.Evaluate(currentFadeTime / fadeTimeDuration);
    }

    public void SetObjectActive(bool enabled)
    {
        if (enabled)
        {
            gameObject.SetActive(true);
            UpdateConfiguration(1, currentFadeTime);
        }
        else
        {
            UpdateConfiguration(-1, currentFadeTime);
        }

        
    }

    private void UpdateConfiguration(int fadeDirection, float fadeTimestamp)
    {
        this.fadeDirection = fadeDirection;
        this.currentFadeTime = fadeTimestamp;

        SetOpacityTo(GetCurrentOpacity());
    }

    public void SetOpacityTo(float opacity)
    {
        foreach (Image image in imagesToFade)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        }

    }

}