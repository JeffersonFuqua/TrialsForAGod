using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    PlayerActions pActions;
    private PlayerValues playerVal;
    private Weapon weaponVal;

    private int attackValue;
    //time if no buttons are pressed to reset the value to 0 to start chain over again
    private float attackValueReset;
    private float attackChainTimer = 1f;
    private bool bAttackChain;


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
            Debug.Log("reset");
        }
    }
    private void PlayerLightAttack(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(lightAttackAction));
    }
    private void PlayerHeavyAttack(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(heavyAttackAction));
    }
    
    IEnumerator lightAttackAction()
    {
        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;

        //reset timer when called
        bAttackChain = false;
        attackValueReset = attackChainTimer;

        yield return new WaitForSeconds(weaponVal.lightAttackStartUp);

        attackValue++;
        if (attackValue == 1)
        {
            Debug.Log(attackValue);
        }
        else if (attackValue == 2)
        {
            Debug.Log(attackValue);
        }
        else if (attackValue == 3)
        {
            Debug.Log(attackValue);
        }

        if (attackValue >= 3)
        {
            attackValue = 0;
        }
        StartCoroutine(lightAttackCooldown());
    }

    IEnumerator lightAttackCooldown()
    {
        yield return new WaitForSeconds(weaponVal.lightAttackCooldown);
        bAttackChain = true;
        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
    }

    IEnumerator heavyAttackAction()
    {
        pActions.PlayerControls.HeavyAttack.performed -= PlayerHeavyAttack;

        bAttackChain = false;
        attackValueReset = attackChainTimer;

        yield return new WaitForSeconds(weaponVal.heavyAttackStartUp);

        attackValue++;
        if (attackValue == 1)
        {
            Debug.Log(attackValue);
        }
        else if (attackValue == 2)
        {
            Debug.Log(attackValue);
        }
        else if (attackValue == 3)
        {
            Debug.Log(attackValue);
        }

        if (attackValue >= 3)
        {
            attackValue = 0;
        }
        StartCoroutine(heavyAttackCooldown());
    }

    IEnumerator heavyAttackCooldown()
    {
        yield return new WaitForSeconds(weaponVal.heavyAttackCooldown);
        bAttackChain = true;
        pActions.PlayerControls.HeavyAttack.performed += PlayerHeavyAttack;
    }
}
