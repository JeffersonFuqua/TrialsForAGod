using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image Healthbar;

    private void OnEnable()
    {
        HealthSystem.UpdateHealthUI += UpdateHealthbar;
    }
    private void OnDisable()
    {
        HealthSystem.UpdateHealthUI -= UpdateHealthbar;
    }
    private void UpdateHealthbar(float value)
    {
        Healthbar.fillAmount = value;
    }
}
