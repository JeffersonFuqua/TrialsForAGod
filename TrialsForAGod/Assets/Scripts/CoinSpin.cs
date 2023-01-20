using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    private float rotRate;

    void FixedUpdate()
    {
        rotRate += 10;
        transform.rotation = Quaternion.Euler(new Vector3(90, rotRate, 0));
    }
}
