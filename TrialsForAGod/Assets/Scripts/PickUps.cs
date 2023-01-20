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

    public static Action<float> UpdateCash = delegate { };
    public static Action<float> UpdateHealth = delegate { };

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
                    Money();
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
    private void Heal(float gain)
    {
        
        UpdateHealth(amount);
    }
    public void Money()
    {
        UpdateCash(amount);
    }
}
