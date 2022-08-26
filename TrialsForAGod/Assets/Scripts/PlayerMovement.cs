using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerValues playerVal;
    PlayerActions pActions;

    private Rigidbody2D rb;
    private Vector3 desiredDirection;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();
    }

    private void OnDisable()
    {
        pActions.Disable();
    }
}
