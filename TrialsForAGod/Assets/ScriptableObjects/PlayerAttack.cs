using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : ScriptableObject
{
    private PlayerValues playerVal;

    PlayerActions pActions;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.PlayerLightAttack.performed += PlayerLightAttack;
        pActions.PlayerControls.PlayerLightAttack.performed += PlayerHeavyAttack;
    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.PlayerLightAttack.performed -= PlayerLightAttack;
        pActions.PlayerControls.PlayerLightAttack.performed -= PlayerHeavyAttack;
    }
    private void PlayerLightAttack(InputAction.CallbackContext c)
    {
        Debug.Log("Light Attack Performed");
    }
    private void PlayerHeavyAttack(InputAction.CallbackContext c)
    {
        Debug.Log("Heavy Attack Performed");
    }
}
