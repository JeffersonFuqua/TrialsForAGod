using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehavior : MonoBehaviour
{
    public bool bSpin;
    private Quaternion qStart, qEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Hit the bone");
        }
    }
    private void Update()
    {
        
    }
}
