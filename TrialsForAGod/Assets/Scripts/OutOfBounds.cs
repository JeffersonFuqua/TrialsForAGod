using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private GameObject player;
    private float boundsLockTime = 1; //0.15f
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.LogError("out of bounds: " + gameObject.name);
            StartCoroutine(boundsLock());
        }
    }

    IEnumerator boundsLock()
    {
        player.GetComponent<PlayerMovement>().Lock();
        yield return new WaitForSeconds(boundsLockTime);
        player.GetComponent<PlayerMovement>().Unlock();

    }
}
