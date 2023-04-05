using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrap : MonoBehaviour
{
    public GameObject lightning;
    private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.gameObject;
            other.GetComponent<PlayerMovement>().Lock();
            other.GetComponent<PlayerMovement>().lockDodge = true;
            other.GetComponent<PlayerAttack>().Lock();
            other.GetComponent<Animator>().SetTrigger("timer");
            StartCoroutine(delayStrike());
        }
    }

    IEnumerator delayStrike()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(strike());
    }
    IEnumerator strike()
    {
        lightning.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        lightning.SetActive(false);
        player.GetComponent<PlayerHealth>().TakeDamageAndKnockback(999, Vector3.zero, true);
    }
}
