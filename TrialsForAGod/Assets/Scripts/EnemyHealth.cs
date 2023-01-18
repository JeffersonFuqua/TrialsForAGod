using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private EnemyValues enemyValues;

    private float maxHealth;
    public float currentHealth;
    public Slider healthBar;
    private bool bInvincible;
    private float stunTime = 0.5f;
    private bool bDead;

    public Material takeDamage;
    private float recievedKnockback;
    private Vector3 difference;
    private Rigidbody rb;
    public GameObject hitMarker;

    private Transform player;

    private void Start()
    {
        enemyValues = GetComponent<EnemyValueHolder>().enemyValues;
        maxHealth = enemyValues.enemyMaxHealth;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
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
        if (bInvincible  || bDead)
            return;
        currentHealth -= damageTaken;
        healthBar.value = currentHealth;
        StartCoroutine(takeKnockback());

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            PlaySound(enemyValues.tookDamageSound);
        }
    }

    public void SwitchMaterial(bool redOn)
    {
        if (redOn)
        {
            GetComponent<MeshRenderer>().material = takeDamage;
        }
        else
        {
            GetComponent<MeshRenderer>().material = enemyValues.baseMaterial;
        }
    }

    public void PlaySound(AudioClip currSound)
    {
        GetComponent<AudioSource>().clip = currSound;
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox") && !bInvincible)
        {
            player = other.transform.root;
            player.GetComponent<PlayerAttack>().PlayerHitStop();
            //applies knockback
            recievedKnockback = player.GetComponent<PlayerAttack>().currentAttackKnockback;
            difference = transform.position - player.position;
            difference.y = 0;
            difference = difference.normalized * recievedKnockback;
            UpdateHealth(other.transform.root.GetComponent<PlayerAttack>().currentAttackDamage);

            Vector3 lookVector = GetComponent<EnemyAI>().aimTool.position - player.transform.position;
            lookVector.y = GetComponent<EnemyAI>().aimTool.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            GetComponent<EnemyAI>().aimTool.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
            hitMarker.GetComponent<ParticleSystem>().Play();
            //StartCoroutine(hitMarkerToggle());

        }

    }

    IEnumerator takeKnockback()
    {
        bInvincible = true;
        StartCoroutine(stun(stunTime));
        rb.AddForce(difference, ForceMode.Impulse);
        SwitchMaterial(true);
        yield return new WaitForSeconds(0.2f);
        SwitchMaterial(false);
        rb.velocity = Vector2.zero;
        bInvincible = false;
    }
    IEnumerator stun(float sTime)
    {
        GetComponent<EnemyAI>().bIsStunned = true;
        yield return new WaitForSeconds(sTime);
        GetComponent<EnemyAI>().bIsStunned = false;
    }

    IEnumerator hitMarkerToggle()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        hitMarker.SetActive(false);
    }

    public void Die()
    {
        bDead = true;
        StartCoroutine(deathDelay());
    }
    IEnumerator deathDelay()
    {
        PlaySound(enemyValues.deathSound);
        if (TryGetComponent<EnemyAI>(out var enemyAI))
        {
            GetComponent<EnemyAI>().enabled = false;
        }
        else if(TryGetComponent<EnemyAIProj>(out var enemyAIProj))
        {
            GetComponent<EnemyAIProj>().enabled = false;
        }
        
        
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

    }
}
