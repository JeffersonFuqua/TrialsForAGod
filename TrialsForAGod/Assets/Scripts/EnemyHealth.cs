using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    //public static Action<float> UpdateHealthUI = delegate { };
    private bool bInvincible;

    private EnemyValues enemyVal;

    private void Start()
    {
        enemyVal = GetComponent<EnemyValueHolder>().enemyVal;
        //UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);
    }

    public void UpdateHealth(int damageTaken)
    {
        if (bInvincible)
            return;

        currentHealth -= damageTaken;
        //UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out TempDamage dVolume))
        {
            UpdateHealth(dVolume.Damage);
        }

    }
}
