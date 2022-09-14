using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interacter : MonoBehaviour
{
    PlayerActions pActions;
    private bool bInteract;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interact") && bInteract)
        {
            //  Debug.Log("Trying to Interact");
            other.gameObject.GetComponent<InteractTalk>().Interacting();
            bInteract = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interact")
        {
              Debug.Log("Left Interactable");
        }
    }
    private void Interact(InputAction.CallbackContext c)
    {
        Debug.Log("Interacting");
        StartCoroutine(InteractCooldown());
    }

    IEnumerator InteractCooldown()
    {
        bInteract = true;
        yield return new WaitForSeconds(0.1f);
        bInteract = false;
    }
}
