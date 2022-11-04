using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisableDialogue : MonoBehaviour
{
    PlayerActions pActions;
    public GameObject dialogueCanvas;

    [HideInInspector]public bool bIsDisabled;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.Pause.started += DisableDia;

    }
    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.Dodge.started -= DisableDia;
    }

    public void DisableDia(InputAction.CallbackContext c)
    {
        if (!bIsDisabled)
        {
            EnableDialogue();
        }
        else
        {
            DisableDiaCanvas();
        }
    }

    public void DisableDiaCanvas()
    {
        dialogueCanvas.SetActive(false);
        bIsDisabled = true;
    }
    public void EnableDialogue()
    {
        dialogueCanvas.SetActive(true);
        bIsDisabled = false;
    }
}
