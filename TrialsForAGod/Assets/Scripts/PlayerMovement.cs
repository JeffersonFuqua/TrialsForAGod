using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerValues playerVal;
    PlayerActions pActions;

    private Rigidbody rb;
    private Vector3 desiredDirection;
    private float speed;
    private MeshRenderer playerColor;

    private float faceRotationSpeed = 8;

    private bool bLocked;

    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        rb = GetComponent<Rigidbody>();
        speed = playerVal.playerSpeed;

        playerColor = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.Dodge.started += Dodge;
    }

    private void OnDisable()
    {
        pActions.Disable();

        pActions.PlayerControls.Dodge.started -= Dodge;
    }

    public void FixedUpdate()
    {
        if (!bLocked)
        {
            Movement();
        }
        //cancels any generated velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Movement()
    {
        desiredDirection.x = pActions.PlayerControls.Movement.ReadValue<Vector2>().x;
        desiredDirection.z = pActions.PlayerControls.Movement.ReadValue<Vector2>().y;

        rb.MovePosition(rb.position + desiredDirection * speed * Time.fixedDeltaTime);

        if (desiredDirection.x != 0 || desiredDirection.z != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
        }
    }

    public void Dodge(InputAction.CallbackContext c)
    {
        if ((pActions.PlayerControls.Movement.ReadValue<Vector2>().x != 0 || pActions.PlayerControls.Movement.ReadValue<Vector2>().y != 0))
        {
            StartCoroutine(dodgeCooldown());
            StartCoroutine(dodgeAction());
        }
    }
    IEnumerator dodgeAction()
    {
        speed += playerVal.dodgeSpeed;
        playerColor.material.color = new Vector4(playerColor.material.color.r, playerColor.material.color.g, playerColor.material.color.b, 0.1f);
        //GetComponent<PlayerHealth>().bInvincible = true;
        yield return new WaitForSeconds(playerVal.dodgeDuration);
        speed = playerVal.playerSpeed;
        playerColor.material.color = new Vector4(playerColor.material.color.r, playerColor.material.color.g, playerColor.material.color.b, 1);
        // GetComponent<PlayerHealth>().bInvincible = false;
    }
    IEnumerator dodgeCooldown()
    {
        pActions.PlayerControls.Dodge.started -= Dodge;
        yield return new WaitForSeconds(playerVal.dodgeCooldown);
        pActions.PlayerControls.Dodge.started += Dodge;
    }

    //to lock and unlock player movement
    public void Lock()
    {
        bLocked = true;
        pActions.PlayerControls.Dodge.started += Dodge;
    }
    public void Unlock()
    {
        bLocked = false;
        pActions.PlayerControls.Dodge.started -= Dodge;
    }
}
