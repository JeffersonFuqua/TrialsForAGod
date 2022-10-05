using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckList : MonoBehaviour
{
    public GameObject[] toDoList;
    public GameObject ListCanvas;
    private int x = 0;
    private int y = 0;
    private PlayerValues playerVal;
    PlayerActions pActions;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
    }

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
                //light attack
                check(toDoList[0]);
                x++;
                if (x == toDoList.Length)
                    ListCanvas.SetActive(false);
                break;
            case 1:
                //heavy attack
                check(toDoList[1]);
                x++;
                if (x == toDoList.Length)
                    ListCanvas.SetActive(false);
                break;
            case 2:
                //dash
                check(toDoList[2]);
                x++;
                if (x == toDoList.Length)
                    ListCanvas.SetActive(false);
                break;
            case 3:
                //chain
                check(toDoList[3]);
                x++;
                if (x == toDoList.Length)
                    ListCanvas.SetActive(false);
                break;
        }
    }
    private void check(GameObject done)
    {
        done.SetActive(false);
    }
    private void Lattack(InputAction.CallbackContext c)
    {
        CheckOff(0);
    }
    private void Hattack(InputAction.CallbackContext c)
    {
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
}
