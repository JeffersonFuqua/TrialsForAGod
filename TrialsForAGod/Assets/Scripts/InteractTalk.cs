using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTalk : MonoBehaviour
{
    // public int type;
    public GameObject dialogueCanvas;
    public void Interacting(int style)
    {
        switch(style)
        {
            case 1:
                //talk
                Talk();
                break;
            case 2:
                //pickup
                Pickup();
                break;
            case 3:
                //heal
                Heal();
                break;
            case 4:
                //door
                Door();
                break;
        }   
    }
    public void Talk()
    {
        Debug.Log("Talk");
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
}
