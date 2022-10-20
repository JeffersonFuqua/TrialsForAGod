using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehavior : MonoBehaviour
{
    private Projectile projVal;

    public bool bSpin;
    private Quaternion qStart, qEnd;

    private void Start()
    {
        projVal = GetComponent<ProjectileValueHolder>().projValues;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit the bone");
            Vector3 difference = other.transform.position - transform.position;
            
            difference = difference.normalized * projVal.projKnocback;
            other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(projVal.projDamage, difference);
            StartCoroutine(deleteProj());
            
        }
    }
    IEnumerator deleteProj()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}
