using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    private PlayerValues playerVal;
    //private bool bLightAttack, bHeavyAttack;
    private int attackValue;

    PlayerActions pActions;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
        //pActions.PlayerControls.PlayerHeavyAttack.performed += PlayerHeavyAttack;
    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        //pActions.PlayerControls.PlayerHeavyAttack.performed -= PlayerHeavyAttack;
    }
    private void PlayerLightAttack(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(lightAttackAction));
    }
    /*
    private void PlayerHeavyAttack(InputAction.CallbackContext c)
    {
        StartCoroutine(nameof(AttackCooldown));
    }
    */
    IEnumerator lightAttackAction()
    {
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

        if(attackValue >= 3)
        {
            attackValue = 0;
        }

        pActions.PlayerControls.LightAttack.performed -= PlayerLightAttack;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(lightAttackCooldown());
    }

     IEnumerator lightAttackCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        pActions.PlayerControls.LightAttack.performed += PlayerLightAttack;
    }
}
