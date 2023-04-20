using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaster : MonoBehaviour
{
    public GameObject keyUI;
    public GameObject blueKeyUI;
    public GameObject redKeyUI;
    private GameObject doorOrKey;
    private bool bHasKey;
    private bool bHasBlueKey;
    private bool bHasRedKey;

    private void OnTriggerEnter(Collider other)
    {
        //key
        if(other.CompareTag("Key"))
        {
            keyUI.SetActive(true);
            bHasKey = true;
            doorOrKey = other.gameObject;
            doorOrKey.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(destroyDelay());
        }
        //blue key
        if (other.CompareTag("BlueKey"))
        {
            blueKeyUI.SetActive(true);
            bHasBlueKey = true;
            doorOrKey = other.gameObject;
            doorOrKey.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(destroyDelay());
        }
        //red key
        if (other.CompareTag("RedKey"))
        {
            redKeyUI.SetActive(true);
            bHasRedKey = true;
            doorOrKey = other.gameObject;
            doorOrKey.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(destroyDelay());
        }

        //door
        if (other.CompareTag("Door") && bHasKey)
        {
            keyUI.SetActive(false);
            bHasKey = false;
            doorOrKey = other.gameObject;
            doorOrKey.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(destroyDelay());
        }
        //blue door
        if (other.CompareTag("BlueDoor") && bHasBlueKey)
        {
            blueKeyUI.SetActive(false);
            bHasBlueKey = false;
            doorOrKey = other.gameObject;
            doorOrKey.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(destroyDelay());
        }
        //red door
        if (other.CompareTag("RedDoor") && bHasRedKey)
        {
            redKeyUI.SetActive(false);
            bHasRedKey = false;
            doorOrKey = other.gameObject;
            doorOrKey.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(destroyDelay());
        }
    }

    IEnumerator destroyDelay()
    {
        doorOrKey.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(doorOrKey);
        GetComponent<PlayerMovement>().lockDodge = false;
    }

}
