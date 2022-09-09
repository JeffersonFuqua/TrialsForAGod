using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    public GameObject lightHitBox1;
    public GameObject lightHitBox2;
    public GameObject lightHitBox3;
    public void HitBoxActive(int actionVal)
    {
        if(actionVal == 1)
        {
            Debug.Log("1");
            lightHitBox1.SetActive(true);
        }
        else if (actionVal == 2)
        {
            Debug.Log("2");
            lightHitBox2.SetActive(true);
        }
        else if(actionVal == 3)
        {
            Debug.Log("3");
            lightHitBox3.SetActive(true);
        }
    }
    public void HitBoxInActive()
    {
        //Debug.Log("off");
        lightHitBox1.SetActive(false);
        lightHitBox2.SetActive(false);
        lightHitBox3.SetActive(false);
    }

}
