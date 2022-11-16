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
            Debug.Log("Entered Interactable");
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

        Debug.Log("Interacting");
        interactableObj.GetComponent<InteractTalk>().Interacting(1);

    }
}
