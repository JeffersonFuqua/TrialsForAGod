using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueCollider : MonoBehaviour
{
    PlayerActions pActions;
    private bool bNear;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.Interact.started += Interact;
        
    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.Interact.started -= Interact;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    private void Interact(InputAction.CallbackContext c)
    {
        if (bNear == true)
        {

        }
    }
}