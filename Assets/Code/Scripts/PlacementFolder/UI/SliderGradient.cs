using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderGradient : MonoBehaviour
{
    [SerializeField] private Gradient gradient = null;
    private float maxSliderValue;
    private float minSliderValue;
    private float sliderValueDistance;
    private float sliderValue;
    private float sliderAddValue;

    private Transform slider;

    private Image image;

    private void Start()
    {
        slider = transform.parent.parent;
        image = GetComponent<Image>();

        maxSliderValue = transform.parent.parent.GetComponent<Slider>().maxValue;
        minSliderValue = transform.parent.parent.GetComponent<Slider>().minValue;
        sliderValueDistance = maxSliderValue - minSliderValue;
        sliderAddValue = 1 - Mathf.Abs(maxSliderValue / sliderValueDistance);

    }

    private void Update()
    {
        float currentSliderValue = slider.GetComponent<Slider>().value;
        
        float gradientValue = (currentSliderValue / sliderValueDistance) + sliderAddValue;
        image.color = gradient.Evaluate(gradientValue);
    }
}
