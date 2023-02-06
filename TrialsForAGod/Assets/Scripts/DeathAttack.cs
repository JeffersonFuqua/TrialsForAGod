using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAttack : MonoBehaviour
{
    public bool slimeDeath;
    public GameObject slimeObject;

    public void HasDied()
    {
        if (slimeDeath)
        {
            SlimeDeath();
        }
    }

    public void SlimeDeath()
    {
        GameObject g = Instantiate(slimeObject);
        g.transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
}
