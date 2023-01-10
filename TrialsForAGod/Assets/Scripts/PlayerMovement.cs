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
    [HideInInspector]public float speed;
    //private MeshRenderer playerColor;

    private float faceRotationSpeed = 8;
    [HideInInspector]public bool bIsRunning;

    private bool bLocked;

    private Animator playerAnim;
    private bool bDodge;


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
            //cancels any generated velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        //keeps the player level
        if(transform.position.y > 0 || transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (!bLocked)
        {
            Movement();
        }
    }
    public void Movement()
    {
        //Debug.LogError("move");
        desiredDirection.x = pActions.PlayerControls.Movement.ReadValue<Vector2>().x;
        desiredDirection.z = pActions.PlayerControls.Movement.ReadValue<Vector2>().y;
        desiredDirection = desiredDirection.normalized;

        rb.MovePosition(rb.position + desiredDirection * speed * Time.fixedDeltaTime);
        //rb.position += desiredDirection * speed * Time.fixedDeltaTime;

        if ((desiredDirection.x != 0 || desiredDirection.z != 0)/* && !GetComponent<PlayerAttack>().bIsAttacking*/)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(desiredDirection.x, 0, desiredDirection.z)), Time.deltaTime * faceRotationSpeed);
            if(!bIsRunning && !bDodge && !GetComponent<PlayerAttack>().bIsAttacking)
            {
                PlaySound(playerVal.runningSFX);
                playerAnim.SetTrigger("running");
                bIsRunning = true;
            }
        }
        else
        {
            if (bIsRunning && !GetComponent<PlayerAttack>().bIsAttacking)
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
            bDodge = true;
            StartCoroutine(dodgeCooldown());
            StartCoroutine(dodgeAction());
        }
    }
    IEnumerator dodgeAction()
    {
        speed = playerVal.dodgeSpeed;
        gameObject.layer = LayerMask.NameToLayer("Dodge");
        GetComponent<PlayerHealth>().bInvincible = true;
        playerAnim.SetBool("dodge", true);
        //playerAnim.SetLayerWeight(1, 1);
        //Debug.Log("dodge");
        yield return new WaitForSeconds(playerVal.dodgeDuration);
        playerAnim.SetBool("dodge", false);
       // playerAnim.SetLayerWeight(1, 0);
        if (bIsRunning)
        {
            //Debug.Log("run");
            playerAnim.SetTrigger("running");
        }
        else
        {
            playerAnim.SetTrigger("timer");
            GetComponent<AudioSource>().Stop();
        }
        bDodge = false;
        speed = playerVal.playerSpeed;
        gameObject.layer = LayerMask.NameToLayer("Player");
        GetComponent<PlayerHealth>().bInvincible = false;
    }
    IEnumerator dodgeCooldown()
    {
        pActions.PlayerControls.Dodge.started -= Dodge;
        yield return new WaitForSeconds(playerVal.dodgeCooldown);
        pActions.PlayerControls.Dodge.started += Dodge;
    }

    public void PlaySound(AudioClip currSound)
    {
        GetComponent<AudioSource>().clip = currSound;
        GetComponent<AudioSource>().Play();
    }
    //to lock and unlock player movement
    public void Lock()
    {
        bLocked = true;
        pActions.PlayerControls.Dodge.started -= Dodge;
    }
    public void Unlock()
    {
        bLocked = false;
        pActions.PlayerControls.Dodge.started += Dodge;
    }
}
