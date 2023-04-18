using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator fade;
    public Slider playerHealthBar;
    private PlayerValues playerVal;

    private Rigidbody rb;

    private float maxHealth;
    public float currentHealth;
    private static float savedHealth;
    public SkinnedMeshRenderer playerSkin;
    public Material takeDamage;
    public bool bInvincible;
    public bool bDead;

    public Volume volume;

    //checks if is tutorial
    public bool bisTutorial;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        maxHealth = playerVal.playerMaxHealth;
        currentHealth = savedHealth;
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = currentHealth;
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        PickUps.UpdateHealth += GainHealth;
        if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            savedHealth = 100;
        }
        if (currentHealth > 30)
        {
            if (volume.profile.TryGet<Vignette>(out Vignette vig))
            {
                vig.intensity.Override(0f);
            }
        }
    }

    private void OnDisable()
    {
        PickUps.UpdateHealth -= GainHealth;
        savedHealth = currentHealth;
    }

    public void TakeDamageAndKnockback(float damage, Vector3 attackOrigin, bool bNoKnockback)
    {
        if (bInvincible || bDead)
            return;

        currentHealth -= damage;
        playerHealthBar.value = currentHealth;

        if (!bNoKnockback)
        {
            StartCoroutine(invincible(1));
            StartCoroutine(playerRecievedKnockback(attackOrigin));
        }
        else
        {
            StartCoroutine(invincible(0.1f));
            if (currentHealth <= 30)
            {
                if (volume.profile.TryGet<Vignette>(out Vignette vig))
                {
                    vig.intensity.Override(0.5f);
                }
            }
            if (currentHealth <= 0)
            {
                bDead = true;
                //StopAllCoroutines();
                StartCoroutine(playerDies());
            }
        }        
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
        if(currentHealth <= 30)
        {
            if (volume.profile.TryGet<Vignette>(out Vignette vig))
            {
                vig.intensity.Override(0.5f);
            }
        }
        if (currentHealth <= 0)
        {
            bDead = true;
            //StopAllCoroutines();
            StartCoroutine(playerDies());
        }
    }
    IEnumerator playerDies()
    {
       
        if (volume.profile.TryGet<Vignette>(out Vignette vig))
        {
            vig.intensity.Override(0f);
        }
        
        GetComponent<Animator>().SetTrigger("dead");

        GetComponent<PlayerMovement>().Lock();
        GetComponent<PlayerAttack>().Lock();
        fade.speed = 0.2f;
        fade.SetTrigger("fadeOut");
        yield return new WaitForSeconds(6);
        Debug.Log("dead");
        if(bisTutorial)
        {
            //first death cutscene
            SceneManager.LoadScene(4);
        }
        else
        {
            //hub area
            SceneManager.LoadScene(6);
        }
        
        
        
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
        if (currentHealth > 30)
        {
            if (volume.profile.TryGet<Vignette>(out Vignette vig))
            {
                vig.intensity.Override(0f);
            }
        }
    }


}
