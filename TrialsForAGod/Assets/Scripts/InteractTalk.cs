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

    public void Interacting(int style)
    {
        switch(style)
        {
            case 1:
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
        Debug.Log("Talk");
        keyboardText.SetActive(false);
        xboxText.SetActive(false);
        player.GetComponent<PlayerMovement>().Lock();
        player.GetComponent<PlayerAttack>().Lock();
        healthCanvas.SetActive(false);
        dialogueCanvas.SetActive(true);
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
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Interacter>().interactVal = type;

            string[] controller = Input.GetJoystickNames();
            for (int j = 0; j < controller.Length; j++)
            {
                if (controller[j].Length == 0)
                {
                    keyboardText.SetActive(true);
                }
                else
                {
                    xboxText.SetActive(true);
                }
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
