using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterCooler : MonoBehaviour
{
    PlayerActions pActions;
    public GameObject levelUpMenu;
    public GameObject e;
    public GameObject a;
    public GameObject levelE;
    public GameObject levelA;

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
            // e.SetActive(true);
            string[] controller = Input.GetJoystickNames();
            for (int j = 0; j < controller.Length; j++)
            {
                if (controller[j].Length == 0)
                {
                    e.SetActive(true);
                    a.SetActive(false);
                }
                else
                {
                    a.SetActive(true);
                    e.SetActive(false);
                }
            }
            pActions.PlayerControls.Interact.started += Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            e.SetActive(false);
            a.SetActive(false);
            pActions.PlayerControls.Interact.started -= Interact;
        }
    }

    public void Interact(InputAction.CallbackContext c)
    {
        if (!bMenuOn)
        {
            player.GetComponent<PlayerMovement>().Lock();
            levelUpMenu.SetActive(true);
            bMenuOn = true;
            string[] controller = Input.GetJoystickNames();
            for (int j = 0; j < controller.Length; j++)
            {
                if (controller[j].Length == 0)
                {
                    levelE.SetActive(true);
                    levelA.SetActive(false);
                }
                else
                {
                    levelA.SetActive(true);
                    levelE.SetActive(false);
                }
            }
        }
        else
        {
            player.GetComponent<PlayerMovement>().Unlock();
            levelUpMenu.SetActive(false);
            bMenuOn = false;
        }
    }
}
