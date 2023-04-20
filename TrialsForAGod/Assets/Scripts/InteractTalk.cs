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
    public bool bHeal;
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
        bhasTalked = true;
        keyboardText.SetActive(false);
        xboxText.SetActive(false);
        player.GetComponent<PlayerMovement>().Lock();
        player.GetComponent<PlayerAttack>().Lock();
        healthCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);
        if (bHeal)
            Heal();
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
        //Debug.Log("heal");
        player.GetComponent<PlayerHealth>().GainHealth(999);
    }
    public void Door()
    {
        Debug.Log("door");
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((bsingle && !bhasTalked) || !bsingle)
        {
            if (other.CompareTag("Player"))
            {
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