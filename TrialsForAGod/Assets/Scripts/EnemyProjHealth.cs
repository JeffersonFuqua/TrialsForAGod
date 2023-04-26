using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProjHealth : MonoBehaviour
{
    private ProjEnemyValues enemyProjVal;

    private float maxHealth;
    public float currentHealth;
    public Slider healthBar;
    private bool bInvincible;
    private bool bDead;

    public Material takeDamage;
    private float recievedKnockback;
    private Vector3 difference;
    private Rigidbody rb;
    public GameObject hitMarker;
    public GameObject overheadLight;

    public GameObject deathEffect;

    private Transform player;
    public GameObject idleHitBox;

    private void Start()
    {
        enemyProjVal = GetComponent<ProjEnemyValueHolder>().projEnemyVal;
        maxHealth = enemyProjVal.enemyMaxHealth;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
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
        if (bInvincible || bDead)
            return;

        //Debug.Log("ouch");
        currentHealth -= damageTaken;
        healthBar.value = currentHealth;
        StartCoroutine(takeKnockback());
        //UpdateHealthUI(currentHealth / playerVal.playerMaxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            int i;
            i = Random.Range(0, enemyProjVal.tookDamageSound.Count);
            
            PlaySound(enemyProjVal.tookDamageSound[i]);
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
            GetComponent<MeshRenderer>().material = enemyProjVal.baseMatarial;
        }
    }

    public void PlaySound(AudioClip currSound)
    {
        GetComponent<AudioSource>().clip = currSound;
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox") && !bInvincible)
        {
            player = other.transform.root;
            player.GetComponent<PlayerAttack>().PlayerHitStop();
            //applies knockback
            recievedKnockback = player.GetComponent<PlayerAttack>().currentAttackKnockback;
            difference = transform.position - player.position;
            difference.y = 0;
            difference = difference.normalized * recievedKnockback;
            UpdateHealth(other.transform.root.GetComponent<PlayerAttack>().currentAttackDamage);

            Vector3 lookVector = player.transform.position - GetComponent<EnemyAIProj>().aimTool.position;
            lookVector.y = GetComponent<EnemyAIProj>().aimTool.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            GetComponent<EnemyAIProj>().aimTool.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
            hitMarker.GetComponent<ParticleSystem>().Play();
        }

    }

    IEnumerator takeKnockback()
    {
        bInvincible = true;
        StartCoroutine(stun());
        rb.AddForce(difference, ForceMode.Impulse);
        SwitchMaterial(true);
        yield return new WaitForSeconds(0.35f);
        SwitchMaterial(false);
        rb.velocity = Vector2.zero;
        bInvincible = false;
    }

    IEnumerator stun()
    {
        GetComponent<EnemyAIProj>().bIsStunned = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<EnemyAIProj>().bIsStunned = false;
    }

    public void Die()
    {
        StopAllCoroutines();
        //this.gameObject.GetComponent<ItemDrop>().SetDrop(this.transform.position);
        this.gameObject.GetComponent<ItemDrop>().DropItem(transform.position);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        idleHitBox.SetActive(false);
        deathEffect.SetActive(true);

        bDead = true;
        StartCoroutine(deathDelay());
    }

    IEnumerator deathDelay()
    {
        PlaySound(enemyProjVal.deathSound);
        if (TryGetComponent<EnemyAI>(out var enemyAI))
        {
            GetComponent<EnemyAI>().enabled = false;
        }
        else if (TryGetComponent<EnemyAIProj>(out var enemyAIProj))
        {
            GetComponent<EnemyAIProj>().enabled = false;
        }

        overheadLight.SetActive(false);
        /*
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        */
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

    }
}
