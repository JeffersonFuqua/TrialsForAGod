using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    private float rotRate;
    public float fLocalRot;
    public float rotSpeed;

    void FixedUpdate()
    {
        rotRate += rotSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(fLocalRot, rotRate, 0));
    }
}
