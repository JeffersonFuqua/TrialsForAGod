using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTalk : MonoBehaviour
{
    public int type;
    public GameObject dialogueCanvas;
    public GameObject healthCanvas;
    public GameObject player;
    public GameObject keyboardText;
    public GameObject xboxText;
    public GameObject canTalk;
    public bool bsingle;
    public bool bhasTalked;
    public GameObject dots;

    public void Start()
    {
        canTalk.SetActive(true);
        if(bsingle && !bhasTalked || !bsingle)
        {
            dots.SetActive(true);
        }
    }
    public void Interacting(int style)
    {
        Debug.Log("We are interacting");
        switch(style)
        {
            case 1:
                if ((bsingle && !bhasTalked) || !bsingle)
                    Talk();
                break;
            case 2:
                Pickup();
                break;
            case 3:
                Heal();
                break;
            case 4:
                Door();
                break;
        }   
    }
    public void Talk()
    {
        Debug.Log("Trying to Talk");
        bhasTalked = true;
        keyboardText.SetActive(false);
        xboxText.SetActive(false);
        player.GetComponent<PlayerMovement>().Lock();
        player.GetComponent<PlayerAttack>().Lock();
        healthCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);

        if (bsingle && bhasTalked)
        {
            canTalk.SetActive(false);
            dots.SetActive(false);
        }

    }
    public void Pickup()
    {
        Debug.Log("pickup");
    }
    public void Heal()
    {
        Debug.Log("heal");
    }
    public void Door()
    {
        Debug.Log("door");
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Player Has entered Collider");
        if ((bsingle && !bhasTalked) || !bsingle)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("We can talk");
                string[] controller = Input.GetJoystickNames();

                if (controller.Length == 0)
                    keyboardText.SetActive(true);
                else
                    xboxText.SetActive(true);

                player.GetComponent<Interacter>().interactVal = type;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyboardText.SetActive(false);
            xboxText.SetActive(false);
        }
    }
}
