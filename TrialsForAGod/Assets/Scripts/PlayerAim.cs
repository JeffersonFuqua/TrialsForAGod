using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public Transform aimTool;
    private Quaternion lookDirection;
    public Camera mainCam;
    private Vector3 newPosition;
    Plane plane = new Plane(Vector3.down, 0);

    private void FixedUpdate()
    {
        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            newPosition = ray.GetPoint(distance);
            newPosition.x = (int)newPosition.x;
            newPosition.y = 0;
            newPosition.z = (int)newPosition.z;

            lookDirection = Quaternion.LookRotation(newPosition);
            aimTool.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 0.5f);
        }
    }
}
