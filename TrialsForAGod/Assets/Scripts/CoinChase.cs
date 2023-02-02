using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChase : MonoBehaviour
{
    public GameObject parentCoin;
    private Transform player;
    private bool bLerp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponentInParent(typeof(Transform)) as Transform;
            bLerp = true;
            Debug.Log("player Detected");
        }
    }
    private void FixedUpdate()
    {
        if(bLerp)
        {
            Debug.Log("moving to player");
            parentCoin.transform.position =  Vector3.Lerp(parentCoin.transform.position, player.position, 0.07f);
        }
    }
}
