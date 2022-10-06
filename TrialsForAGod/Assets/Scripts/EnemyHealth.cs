using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyValues enemyVal;

    private float maxHealth;
    public float currentHealth;
    //public static Action<float> UpdateHealthUI = delegate { };
    private bool bInvincible;

    private float recievedKnockback;
    private Vector3 difference;
    private Rigidbody rb;

    private Transform player;

    private void Start()
    {
        enemyVal = GetComponent<EnemyValueHolder>().enemyVal;
        maxHealth = enemyVal.enemyMaxHealth;
        currentHealth = maxHealth;
        //UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //allows knockback but no generating velocity
        if (!bInvincible)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void UpdateHealth(float damageTaken)
    {
        if (bInvincible)
            return;

        Debug.Log("ouch");
        currentHealth -= damageTaken;
        StartCoroutine(takeKnockback());
        //UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox") && !bInvincible)
        {
            player = other.transform.root;
            //applies knockback
            recievedKnockback = player.GetComponent<PlayerAttack>().currentAttackKnockback;
            difference = transform.position - player.position;
            difference = difference.normalized * recievedKnockback;
            UpdateHealth(other.transform.root.GetComponent<PlayerAttack>().currentAttackDamage);
        }

    }

    IEnumerator takeKnockback()
    {
        bInvincible = true;
        //rb.isKinematic = false;
        difference.y = 0;
        rb.AddForce(difference, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        //rb.isKinematic = true;
        bInvincible = false;
    }

    public void Die()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
}
