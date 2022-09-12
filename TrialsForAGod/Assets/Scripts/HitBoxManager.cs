using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    public GameObject lightHitBox1;
    public GameObject lightHitBox2;
    public GameObject lightHitBox3;

    public GameObject heavyHitBox1;
    public GameObject heavyHitBox2;
    public GameObject heavyHitBox3;
    public void HitBoxActive(int actionVal)
    {
        if(actionVal == 1)
        {
            lightHitBox1.SetActive(true);
        }
        else if (actionVal == 2)
        {
            lightHitBox2.SetActive(true);
        }
        else if(actionVal == 3)
        {
            lightHitBox3.SetActive(true);
        }
        else if (actionVal == 4)
        {
            heavyHitBox1.SetActive(true);
        }
        else if (actionVal == 5)
        {
            heavyHitBox2.SetActive(true);
        }
        else if (actionVal == 6)
        {
            heavyHitBox3.SetActive(true);
        }
    }
    public void HitBoxInActive()
    {
        //Debug.Log("off");
        lightHitBox1.SetActive(false);
        lightHitBox2.SetActive(false);
        lightHitBox3.SetActive(false);
        heavyHitBox1.SetActive(false);
        heavyHitBox2.SetActive(false);
        heavyHitBox3.SetActive(false);
    }

}
