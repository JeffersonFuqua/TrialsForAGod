using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth;
    public static Action<float> UpdateHealthUI = delegate { };
    private bool bInvincible;

    private PlayerValues playerVal;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);
    }

    public void UpdateHealth(int damageTaken)
    {
        if (bInvincible)
            return;

        currentHealth -= damageTaken;
        UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent(out TempDamage dVolume))
        {
            UpdateHealth(dVolume.Damage);
        }
        
    }
}
