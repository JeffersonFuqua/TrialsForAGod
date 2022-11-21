using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableController : MonoBehaviour
{
    public GameObject controllerVersion;
    public GameObject keyboardVersion;

    void Update()
    {
        string[] controller = Input.GetJoystickNames();

        if (controller.Length == 0)
        {
            keyboardVersion.SetActive(true);
            controllerVersion.SetActive(false);
        }
        else
        {
            controllerVersion.SetActive(true);
            keyboardVersion.SetActive(false);
        }

    }
}
