using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("out of bounds");
            StartCoroutine(boundsLock());
        }
    }

    IEnumerator boundsLock()
    {
        player.GetComponent<PlayerMovement>().Lock();
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerMovement>().Unlock();

    }
}
