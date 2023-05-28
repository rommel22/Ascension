using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int maxHealth)
    {
        Debug.Log(maxHealth);
        slider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
