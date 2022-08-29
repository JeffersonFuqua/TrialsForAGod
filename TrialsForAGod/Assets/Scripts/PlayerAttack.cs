using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    private PlayerValues playerVal;
    private bool bLightAttack, bHeavyAttack;

    PlayerActions pActions;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.PlayerLightAttack.performed += PlayerLightAttack;
        pActions.PlayerControls.PlayerHeavyAttack.performed += PlayerHeavyAttack;
    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.PlayerLightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.PlayerHeavyAttack.performed -= PlayerHeavyAttack;
    }
    private void PlayerLightAttack(InputAction.CallbackContext c)
    {
        if (bLightAttack || bHeavyAttack) return;

        bLightAttack = true;
        StartCoroutine(nameof(AttackCooldown));
    }
    private void PlayerHeavyAttack(InputAction.CallbackContext c)
    {
        if (bLightAttack || bHeavyAttack) return;

        bHeavyAttack = true;
        StartCoroutine(nameof(AttackCooldown));
    }

     IEnumerator AttackCooldown()
    {
        if(bLightAttack)
            Debug.Log("Light Attack Performed");

        if(bHeavyAttack)
            Debug.Log("Heavy Attack Performed");

        yield return new WaitForSeconds(0.5f);
        bLightAttack = false;
        bHeavyAttack = false;
    }
}
