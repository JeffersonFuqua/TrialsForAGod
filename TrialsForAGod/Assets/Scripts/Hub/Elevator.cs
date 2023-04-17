using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Elevator : MonoBehaviour
{
    PlayerActions pActions;
    public GameObject e;

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
        player.GetComponent<PlayerMovement>().Lock();
        player.GetComponent<PlayerAttack>().Lock();
        GetComponent<NextScene>().ChangeScene();
        pActions.PlayerControls.Interact.started -= Interact;
    }
}
