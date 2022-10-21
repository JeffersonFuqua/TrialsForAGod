using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehavior : MonoBehaviour
{
    private Projectile projVal;
    //public GameObject self;
    public int speed;
    private float rotRate;

    public bool bSpin;
    private Quaternion qStart; 
    public Quaternion qEnd;
    private Quaternion qTemp;

    private void Start()
    {
        projVal = GetComponent<ProjectileValueHolder>().projValues;
        qStart = transform.rotation;
        qTemp = qEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit the bone");
            Vector3 difference = other.transform.position - transform.position;

            difference.y = other.transform.position.y;
            difference = difference.normalized * projVal.projKnocback;
            other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(projVal.projDamage, difference);
            //i like to delete things at the end of the frame to prevent any inconsistancies with deleting things at the same time as funtions running
            StartCoroutine(deleteProj());
            
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Geo"))
        {
            StartCoroutine(deleteProj());
        }
    }
    IEnumerator deleteProj()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {

        if (bSpin)
            rotate();
    }
    private void rotate()
    {
        rotRate += 10;
        transform.rotation = Quaternion.Euler(new Vector3(90, rotRate, 0));
    }
}
