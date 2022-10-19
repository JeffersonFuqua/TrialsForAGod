using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CheckList : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public GameObject[] toDoList;
    public RawImage[] listImages;
    public GameObject listCanvas;
    private int x = 0;
    private int y = 0;
    private bool bLat, bHat, bDas, bChain;
  //  private int chainVal;
    PlayerActions pActions;
    public ControllerImages keyboard;
    public ControllerImages control;

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();
        pActions.PlayerControls.Dodge.started += Dodged;
        pActions.PlayerControls.LightAttack.started += Lattack;
        pActions.PlayerControls.HeavyAttack.started += Hattack;
    }

    private void OnDisable()
    {
        pActions.Disable();
        pActions.PlayerControls.Dodge.started -= Dodged;
        pActions.PlayerControls.LightAttack.started -= Lattack;
        pActions.PlayerControls.HeavyAttack.started -= Hattack;
    }

    public void CheckOff(int item)
    {
      switch(item)
       {
            case 0:
                if (bLat)
                    return;

                check(toDoList[0], listImages[0]);
                x++;

                if (x == toDoList.Length)
                    listCanvas.SetActive(false);

                bLat = true;
                break;

            case 1:
                if (bHat)
                    return;

                check(toDoList[1], listImages[1]);
                x++;

                if (x == toDoList.Length)
                    listCanvas.SetActive(false);

                bHat = true;
                break;

            case 2:
                if (bDas)
                    return;

                check(toDoList[2], listImages[2]);
                x++;

                if (x == toDoList.Length)
                    listCanvas.SetActive(false);

                bDas = true;
                break;

            case 3:
                if (bChain)
                    return;
                check(toDoList[3], listImages[3]);
                x++;

                if (x == toDoList.Length)
                    listCanvas.SetActive(false);

                bChain = true;
                break;
        }
    }
    private void check(GameObject done, RawImage finish)
    {
        done.SetActive(false);
        finish.enabled = false;
    }
    private void Lattack(InputAction.CallbackContext c)
    {

        if (playerAttack.bAttackChain)
            CheckOff(3);
        else
            CheckOff(0);
    }
    private void Hattack(InputAction.CallbackContext c)
    {

        if (playerAttack.bAttackChain)
            CheckOff(3);
        else
            CheckOff(1);
    }
    private void Dodged(InputAction.CallbackContext c)
    {
        CheckOff(2);
    }

    public void Increment()
    {
        CheckOff(y);
        y++;
    }
    private void Update()
    {
        string[] controller = Input.GetJoystickNames();
        for(int j = 0; j < controller.Length; j++)
        {
            if(controller[j].Length == 0)
            {
                listImages[0].texture = keyboard.light;
                listImages[1].texture = keyboard.heavy;
                listImages[2].texture = keyboard.dash;
                listImages[3].texture = keyboard.chain;
            }
            else
            {
                listImages[0].texture = control.light;
                listImages[1].texture = control.heavy;
                listImages[2].texture = control.dash;
                listImages[3].texture = control.chain;
            }
        }
    }
    public void DisplayInputs(int value)
    {
        Debug.Log("Controller Detected");

    }
}
