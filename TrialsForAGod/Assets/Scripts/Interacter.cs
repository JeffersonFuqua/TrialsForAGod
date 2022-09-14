using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
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
        if (other.CompareTag("Interact"))
        {
            Debug.Log("Trying to Interact");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interact")
        {

        }
    }
    private void Interact(InputAction.CallbackContext c)
    {

    }
}
