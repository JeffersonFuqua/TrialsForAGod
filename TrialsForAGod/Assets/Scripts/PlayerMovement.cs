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
    //private MeshRenderer playerColor;

    private float faceRotationSpeed = 8;
    [HideInInspector]public bool bIsAttacking;
    [HideInInspector]public bool bIsRunning;

    private bool bLocked;

    private Animator playerAnim;


    private void Start()
    {
        playerVal = GetComponent<PlayerValueHolder>().playerVal;
        rb = GetComponent<Rigidbody>();
        speed = playerVal.playerSpeed;

        playerAnim = GetComponent<PlayerValueHolder>().playerAnim;
        //playerColor = GetComponent<MeshRenderer>();
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
            //cancels any generated velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        //keeps the player level
        if(transform.position.y > 0 || transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    public void Movement()
    {
        desiredDirection.x = pActions.PlayerControls.Movement.ReadValue<Vector2>().x;
        desiredDirection.z = pActions.PlayerControls.Movement.ReadValue<Vector2>().y;
        desiredDirection = desiredDirection.normalized;

        rb.MovePosition(rb.position + desiredDirection * speed * Time.fixedDeltaTime);
        //rb.position += desiredDirection * speed * Time.fixedDeltaTime;

        if ((desiredDirection.x != 0 || desiredDirection.z != 0)/* && !bIsAttacking*/)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
            if(!bIsRunning)
            {
                playerAnim.SetTrigger("running");
                bIsRunning = true;
            }
        }
        else
        {
            if (bIsRunning && !bIsAttacking)
            {
                playerAnim.SetTrigger("timer");
                bIsRunning = false;
            }
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
        speed = playerVal.dodgeSpeed;
        GetComponent<PlayerHealth>().bInvincible = true;
        playerAnim.SetTrigger("dodge");
        yield return new WaitForSeconds(playerVal.dodgeDuration);
        if (bIsRunning)
        {
            playerAnim.SetTrigger("running");
        }
        else
        {
            playerAnim.SetTrigger("timer");
        }
        speed = playerVal.playerSpeed;
        GetComponent<PlayerHealth>().bInvincible = false;
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
