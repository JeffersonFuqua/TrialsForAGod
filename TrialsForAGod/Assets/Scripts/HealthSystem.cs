using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth;
    public static Action<float> UpdateHealthUI = delegate { };
    private bool bInvincible;
    public bool bPlayer;

    private PlayerValues playerVal;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        if(bPlayer)
        {
            UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);
        }
    }

    public void UpdateHealth(int value)
    {
        if (bInvincible)
            return;

        currentHealth = Mathf.Clamp(currentHealth + value, 0, (int)playerVal.playerMaxHealth);
        /*
         * If we are using this for enemy health/boss health we may need to have another int for  
         * Max health and add an else statement to the below if statement for updating health
        */
        if(bPlayer)
        {
            UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);
        }
        //Add in if for when currentHealth == 0
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.TryGetComponent(out TempDamage dVolume))
        {
            UpdateHealth(dVolume.Damage);
        }
        
    }
}
