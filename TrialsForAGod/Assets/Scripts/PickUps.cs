using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public float amount;
    public float duration;
    public int type;
    public GameObject player;
    public PlayerHealth pHealth;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch(type)
            {
                case 0:
                    Heal(amount);
                    this.gameObject.SetActive(false);
                    break;
                case 1:
                    Money(amount);
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
    private void Heal(float gain)
    {
        pHealth.GainHealth(gain);
        Debug.Log("Healing");
    }
    private void Money(float income)
    {

    }
}
