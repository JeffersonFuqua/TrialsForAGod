using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public GameObject Axe, Hammer, Whip;
    private Weapon currentWeaponVal;

    private void Start()
    {
       currentWeaponVal = GetComponent<PlayerValueHolder>().currentWeaponVal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentWeaponVal.weaponName == "Love Whip") 
        {
            Axe.SetActive(false);
            Hammer.SetActive(false);
            Whip.SetActive(true);
        }
        else if(currentWeaponVal.weaponName == "Fire Hammer")
        {
            Axe.SetActive(false);
            Hammer.SetActive(true);
            Whip.SetActive(false);

        } else if(currentWeaponVal.weaponName == "War Axe")
        {
            Axe.SetActive(true);
            Hammer.SetActive(false);
            Whip.SetActive(false);
        }
    }
}
