using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Elevator : MonoBehaviour
{
    PlayerActions pActions;
    public GameObject e;
    public GameObject a;

    private GameObject player;

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
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
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
        player.GetComponent<PlayerMovement>().Lock();
        GetComponent<NextScene>().ChangeScene();
        pActions.PlayerControls.Interact.started -= Interact;
    }
}
