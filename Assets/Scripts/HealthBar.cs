using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image image;
    // Start is called before the first frame update
    public void set_max(float value)
    {
        slider.maxValue = value;
        image.color = gradient.Evaluate(1f);
        slider.value = value;
    }
    public void set_value(float value)
    {
        slider.value = value;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }
}
