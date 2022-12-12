using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListHandler : MonoBehaviour
{
    public GameObject checkList;
    public GameObject gPauseMenu;
    private void Update()
    {
        if (gPauseMenu.GetComponent<PauseMenu>().bIsPaused == true)
        {
            checkList.SetActive(false);
            checkList.GetComponent<CheckList>().bPause = true;
        }
        else if (!checkList.GetComponent<CheckList>().bFin)
        {
            checkList.SetActive(true);
        }
        if (gPauseMenu.GetComponent<PauseMenu>().bIsPaused == false)
            checkList.GetComponent<CheckList>().bPause = false;
    }
}