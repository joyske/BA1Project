using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderGradient : MonoBehaviour
{
    [SerializeField] private Gradient gradient = null;
    private float maxSliderValue;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        
    }

    private void Update()
    {
        maxSliderValue = transform.parent.parent.GetComponent<Slider>().maxValue;
        float currentSliderValue = transform.parent.parent.GetComponent<Slider>().value;
        image.color = gradient.Evaluate( currentSliderValue / maxSliderValue);
    }
}
