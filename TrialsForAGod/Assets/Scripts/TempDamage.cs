using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamage : MonoBehaviour
{
    /*
    [SerializeField]
    protected int damage;
    */

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamageAndKnockback(damage, Vector3.zero);
        }
    }
}
