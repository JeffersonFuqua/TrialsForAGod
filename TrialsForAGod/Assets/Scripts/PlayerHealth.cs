using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public GameObject pauseMenu;
    public Slider playerHealthBar;
    private PlayerValues playerVal;

    private Rigidbody rb;

    private float maxHealth;
    public float currentHealth;
    public SkinnedMeshRenderer playerSkin;
    public Material takeDamage;
    public bool bInvincible;
    public bool bDead;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        maxHealth = playerVal.playerMaxHealth;
        currentHealth = maxHealth;
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = currentHealth;
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        PickUps.UpdateHealth += GainHealth;
    }

    private void OnDisable()
    {
        PickUps.UpdateHealth -= GainHealth;   
    }

    public void TakeDamageAndKnockback(float damage, Vector3 attackOrigin, bool bNoKnockback)
    {
        if (bInvincible || bDead)
            return;

        if(!bNoKnockback)
        {
            StartCoroutine(invincible(1));
            StartCoroutine(playerRecievedKnockback(attackOrigin));
        }
        else
        {
            StartCoroutine(invincible(0.1f));
        }

        currentHealth -= damage;
        playerHealthBar.value = currentHealth;

        
    }
    //recieved damage
    public void SwitchMaterial(bool redOn)
    {
        if (redOn)
        {
            playerSkin.material = takeDamage;
        }
        else
        {
            playerSkin.material = playerVal.playerBaseMaterial;
        }
    }
    IEnumerator playerRecievedKnockback(Vector3 attackOrgin)
    {
        GetComponent<PlayerMovement>().Lock();
        GetComponent<PlayerAttack>().Lock();
        rb.AddForce(attackOrgin, ForceMode.Impulse);
        SwitchMaterial(true);
        yield return new WaitForSeconds(0.4f);
        SwitchMaterial(false);
        rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().Unlock();
        GetComponent<PlayerAttack>().Unlock();
        if (currentHealth <= 0)
        {
            bDead = true;
            //StopAllCoroutines();
            StartCoroutine(playerDies());
        }
    }
    IEnumerator playerDies()
    {
        GetComponent<Animator>().SetTrigger("dead");

        GetComponent<PlayerMovement>().Lock();
        GetComponent<PlayerAttack>().Lock();
        yield return new WaitForSeconds(2);
        Debug.Log("dead");
        bDead = false;
        GetComponent<ResetDelegate>().bcallReset = true;
        GetComponent<PlayerAttack>().Unlock();
        GetComponent<PlayerMovement>().Unlock();

        GetComponent<Animator>().SetTrigger("timer");
    }
    //Invincibility and knockback are seperate unlike with enemys as this will give the player a chance to escape
    IEnumerator invincible(float c)
    {
        bInvincible = true;
        yield return new WaitForSeconds(c);
        bInvincible = false;
    }

    public void GainHealth(float heal)
    {
        currentHealth += heal;
        playerHealthBar.value = currentHealth;
        Debug.Log("yum");
    }


}
