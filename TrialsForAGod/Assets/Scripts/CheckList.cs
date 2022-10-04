using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour
{
    public GameObject[] toDoList;
    public int x = 0;
    public void CheckOff(int item)
    {
      switch(item)
       {
            case 0:
                //light attack
                check(toDoList[0]);
                break;
            case 1:
                //heavy attack
                check(toDoList[1]);
                break;
            case 2:
                //dash
                check(toDoList[2]);
                break;
            case 3:
                //chain
                check(toDoList[3]);
                break;
        }
    }
    private void check(GameObject done)
    {
        done.SetActive(false);
    }
    public void Increment()
    {
        CheckOff(x);
        x++;
    }
}
