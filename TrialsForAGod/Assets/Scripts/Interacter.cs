using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    PlayerActions pActions;
    private bool bInteract;

    private GameObject interactableObj;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();
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
            interactableObj = other.gameObject;
            pActions.PlayerControls.Interact.started += Interact;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interact"))
        {
            pActions.PlayerControls.Interact.started -= Interact;
            Debug.Log("Left Interactable");
        }
    }
    private void Interact(InputAction.CallbackContext c)
    {
        pActions.PlayerControls.Interact.started -= Interact;

        interactableObj.GetComponent<InteractTalk>().Interacting();
        Debug.Log("Interacting");
    }
}
