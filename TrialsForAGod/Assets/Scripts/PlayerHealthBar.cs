using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image Healthbar;

    private void OnEnable()
    {
        PlayerHealth.UpdateHealthUI += UpdateHealthbar;
    }
    private void OnDisable()
    {
        PlayerHealth.UpdateHealthUI -= UpdateHealthbar;
    }
    private void UpdateHealthbar(float currentHealth)
    {
        Healthbar.fillAmount = currentHealth;
    }
}