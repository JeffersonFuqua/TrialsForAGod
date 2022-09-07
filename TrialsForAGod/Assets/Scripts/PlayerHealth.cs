using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider playerHealthBar;
    private PlayerValues playerVal;

    private float maxHealth;
    public float currentHealth;
    public bool bInvincible;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        maxHealth = playerVal.playerMaxHealth;
        currentHealth = maxHealth;
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (bInvincible)
            return;

        currentHealth -= damage;
        playerHealthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            Debug.Log("dead");
        }
    }

    public void GainHealth(float heal)
    {
        currentHealth += heal;
        playerHealthBar.value = currentHealth;
        Debug.Log("yum");
    }


}
