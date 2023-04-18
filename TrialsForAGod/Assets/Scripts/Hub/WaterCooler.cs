using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterCooler : MonoBehaviour
{
    PlayerActions pActions;
    public GameObject levelUpMenu;
    public GameObject e;

    private GameObject player;

    private bool bMenuOn;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();
    }

    private void OnDisable()
    {
        pActions.PlayerControls.Interact.started -= Interact;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            e.SetActive(true);
            pActions.PlayerControls.Interact.started += Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            e.SetActive(false);
            pActions.PlayerControls.Interact.started -= Interact;
        }
    }

    public void Interact(InputAction.CallbackContext c)
    {
        if(!bMenuOn)
        {
            player.GetComponent<PlayerMovement>().Lock();
            levelUpMenu.SetActive(true);
            bMenuOn = true;
        }
        else
        {
            player.GetComponent<PlayerMovement>().Unlock();
            levelUpMenu.SetActive(false);
            bMenuOn = false;
        }
    }
}
