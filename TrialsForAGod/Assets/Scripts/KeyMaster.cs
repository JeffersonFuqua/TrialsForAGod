using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaster : MonoBehaviour
{
    public GameObject keyUI;
    private bool bHasKey;
    private GameObject doorOrKey;

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
        //door
        if(other.CompareTag("Door") && bHasKey)
        {
            keyUI.SetActive(false);
            bHasKey = false;
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
    }

}
