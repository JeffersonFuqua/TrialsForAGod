using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    PlayerActions pActions;
    private PlayerValues playerVal;
    private Weapon weaponVal;
    public GameObject lineRender;

    [HideInInspector]public int attackValue;
    //time if no buttons are pressed to reset the value to 0 to start chain over again
    private float attackValueReset;
    private float attackChainTimer = 0.5f;
    [HideInInspector] public bool bIsAttacking;
    [HideInInspector] public bool bAttackChain;
    [HideInInspector] public float currentAttackDamage;
    [HideInInspector] public float currentAttackKnockback;
    [HideInInspector] public float currentAttackCooldown;
    private float hitStopAddition = 0;

    //public Transform aimTool;
    //private Vector3 aimDirection;

    private bool bLocked;
    private Animator playerAnim;
    private float animDefaultSpeed;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed += PlayerHeavyAttack;
    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed -= PlayerHeavyAttack;
    }
    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        weaponVal = GetComponent<PlayerValueHolder>().currentWeaponVal;

        attackValueReset = attackChainTimer;

        playerAnim = GetComponent<PlayerValueHolder>().playerAnim;
        animDefaultSpeed = playerAnim.speed;

        lineRender.SetActive(false);
    }

    private void Update()
    {
        if (bAttackChain && attackValueReset > 0)
        {
            attackValueReset -= Time.deltaTime;
        }
        if(attackValueReset <= 0)
        {
            bAttackChain = false;
            attackValueReset = attackChainTimer;
            attackValue = 0;

            if (GetComponent<PlayerMovement>().bIsRunning)
            {
                playerAnim.SetTrigger("running");
            }
            else
            {
                playerAnim.SetTrigger("timer");
            }
            //Debug.Log("reset");
        }

    }
    private void PlayerLightAttack(InputAction.CallbackContext c)
    {
        //slows player when attacking
        GetComponent<PlayerMovement>().speed = playerVal.playerSpeed / 2;
        StartCoroutine(nameof(lightAttackAction));
    }
    private void PlayerHeavyAttack(InputAction.CallbackContext c)
    {
        //slows player when attacking
        GetComponent<PlayerMovement>().speed = playerVal.playerSpeed / 2;
        StartCoroutine(nameof(heavyAttackAction));
    }
    
    IEnumerator lightAttackAction()
    {
        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed -= PlayerHeavyAttack;
        bIsAttacking = true;

        lineRender.SetActive(true);
        //reset timer when called
        bAttackChain = false;
        attackValueReset = attackChainTimer;

        if (!bLocked)
        {
            attackValue++;
            if (attackValue == 1)
            {
                //light 1 anim
                currentAttackDamage = weaponVal.lightDamage1;
                currentAttackKnockback = weaponVal.lightKnockback1;
                currentAttackCooldown = weaponVal.lightCooldown1;
                playerAnim.SetTrigger("light1");
                //Debug.Log(attackValue + " light");
            }
            else if (attackValue == 2)
            {
                //light 2 anim
                currentAttackDamage = weaponVal.lightDamage2;
                currentAttackKnockback = weaponVal.lightKnockback2;
                currentAttackCooldown = weaponVal.lightCooldown2;
                playerAnim.SetTrigger("light2");
                //Debug.Log(attackValue + " light");
            }
            else if (attackValue == 3)
            {
                //light 3 anim
                currentAttackDamage = weaponVal.lightDamage3;
                currentAttackKnockback = weaponVal.lightKnockback3;
                currentAttackCooldown = weaponVal.lightCooldown3;
                playerAnim.SetTrigger("light3");
                //Debug.Log(attackValue + " light");
            }

        }

        yield return new WaitForSeconds(weaponVal.lightStartUp + hitStopAddition);

        StartCoroutine(lightAttackCooldown());

        if (attackValue >= 3)
        {
            attackValue = 0;
        }
    }

    IEnumerator lightAttackCooldown()
    {
        yield return new WaitForSeconds(currentAttackCooldown + hitStopAddition);
        //sets speed back to normal after attack is done
        GetComponent<PlayerMovement>().speed = playerVal.playerSpeed;
        lineRender.SetActive(false);
        bAttackChain = true;
        bIsAttacking = false;
        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed += PlayerHeavyAttack;
    }

    IEnumerator heavyAttackAction()
    {
        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed -= PlayerHeavyAttack;
        bIsAttacking = true;

        lineRender.SetActive(true);

        bAttackChain = false;
        attackValueReset = attackChainTimer;

        if (!bLocked)
        {
            attackValue++;
            if (attackValue == 1)
            {
                //heavy 1 anim
                currentAttackDamage = weaponVal.heavyDamage1;
                currentAttackKnockback = weaponVal.heavyKnockback1;
                currentAttackCooldown = weaponVal.heavyCooldown1;
                playerAnim.SetTrigger("heavy1");
                //Debug.Log(attackValue + " heavy");
            }
            else if (attackValue == 2)
            {
                //heavy 2 anim
                currentAttackDamage = weaponVal.heavyDamage2;
                currentAttackKnockback = weaponVal.heavyKnockback2;
                currentAttackCooldown = weaponVal.heavyCooldown2;
                playerAnim.SetTrigger("heavy2");
                //Debug.Log(attackValue + " heavy");
            }
            else if (attackValue == 3)
            {
                //heavy 3 anim
                currentAttackDamage = weaponVal.heavyDamage3;
                currentAttackKnockback = weaponVal.heavyKnockback3;
                currentAttackCooldown = weaponVal.heavyCooldown3;
                playerAnim.SetTrigger("heavy3");
                //Debug.Log(attackValue + " heavy");
            }

        }

        yield return new WaitForSeconds(weaponVal.heavyStartUp + hitStopAddition);

        StartCoroutine(heavyAttackCooldown());

        if (attackValue >= 3)
        {
            attackValue = 0;
        }

    }

    IEnumerator heavyAttackCooldown()
    {
        yield return new WaitForSeconds(currentAttackCooldown + hitStopAddition);
        //sets speed back to normal after attack is done
        GetComponent<PlayerMovement>().speed = playerVal.playerSpeed;
        lineRender.SetActive(false);
        bAttackChain = true;
        bIsAttacking = false;
        pActions.PlayerControls.HeavyAttack.performed += PlayerHeavyAttack;
        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
    }

    public void PlayerHitStop()
    {
        StartCoroutine(hitStopTimer());
    }
    IEnumerator hitStopTimer()
    {
        playerAnim.speed = 0;
        hitStopAddition = 0.5f;
        yield return new WaitForSeconds(0.1f);
        hitStopAddition = 0;
        playerAnim.speed = animDefaultSpeed;
    }

    //to lock and unlock player attack
    public void Lock()
    {
        bLocked = true;
        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed -= PlayerHeavyAttack;
    }
    public void Unlock()
    {
        bLocked = false;
        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
        pActions.PlayerControls.HeavyAttack.performed += PlayerHeavyAttack;
    }
}
