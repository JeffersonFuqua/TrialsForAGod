using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUps : MonoBehaviour
{
    public float amount;
    public int type;
    public GameObject player;
    private PlayerHealth pHealth;

    public AudioClip[] aPickup;
    private int irand;

    public static Action<float> UpdateCash = delegate { };
    public static Action<float> UpdateHealth = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            irand = UnityEngine.Random.Range(0, aPickup.Length);
            GetComponent<AudioSource>().clip = aPickup[irand];
            GetComponent<AudioSource>().Play();
            new WaitForSeconds(1);
            switch (type)
            {
                case 0:
                    Heal(amount);
                    break;
                case 1:
                    Money();
                    break;
            }
            StartCoroutine(nameof(PickupDelay));
        }
    }
    private void Heal(float gain)
    {
        UpdateHealth(amount);
    }
    public void Money()
    {
        UpdateCash(amount);
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    IEnumerator PickupDelay()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
