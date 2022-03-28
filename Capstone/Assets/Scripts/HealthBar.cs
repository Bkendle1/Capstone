using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //Setting the fill to be the color at the specified point in the gradient
        fill.color = gradient.Evaluate(1f);
        
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        
        //normalizedValue translates whatever the range values of the slider is to 0 and 1
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
