using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YtoSkipDisplay : MonoBehaviour
{
    public GameObject ShowHide;
    void Update()
    {
        string[] controller = Input.GetJoystickNames();
        if (controller.Length == 0)
        {
            ShowHide.SetActive(false);
        }
        else
        {
            ShowHide.SetActive(true);
        }
    }
}
