using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjHealth : MonoBehaviour
{
    private ProjEnemyValues enemyProjVal;

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
        enemyProjVal = GetComponent<ProjEnemyValueHolder>().projEnemyVal;
        maxHealth = enemyProjVal.enemyMaxHealth;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox") && !bInvincible)
        {
            player = other.transform.root;
            //applies knockback
            recievedKnockback = player.GetComponent<PlayerAttack>().currentAttackKnockback;
            difference = transform.position - player.position;
            difference.y = 0;
            difference = difference.normalized * recievedKnockback;
            UpdateHealth(other.transform.root.GetComponent<PlayerAttack>().currentAttackDamage);
        }

    }

    IEnumerator takeKnockback()
    {
        bInvincible = true;
        //rb.isKinematic = false;
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
