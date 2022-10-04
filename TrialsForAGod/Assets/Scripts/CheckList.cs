using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
    public GameObject[] toDoList;
    public GameObject ListCanvas;
    private int x = 0;
    private int y = 0;
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
    public void Increment()
    {
        CheckOff(y);
        y++;
    }
}
