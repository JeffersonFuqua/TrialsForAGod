using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public GameObject Sword, Hammer, Whip;
    private Weapon currentWeaponVal;

    private void Start()
    {
        currentWeaponVal = GetComponent<PlayerValueHolder>().currentWeaponVal;

        if (currentWeaponVal.weaponName == "Love Whip")
        {
            Sword.SetActive(false);
            Hammer.SetActive(false);
            Whip.SetActive(true);
        }
        else if (currentWeaponVal.weaponName == "Fire Hammer")
        {
            Sword.SetActive(false);
            Hammer.SetActive(true);
            Whip.SetActive(false);

        }
        else if (currentWeaponVal.weaponName == "War Sword")
        {
            Sword.SetActive(true);
            Hammer.SetActive(false);
            Whip.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        currentWeaponVal = GetComponent<PlayerValueHolder>().currentWeaponVal;

        if (currentWeaponVal.weaponName == "Love Whip") 
        {
            Sword.SetActive(false);
            Hammer.SetActive(false);
            Whip.SetActive(true);
        }
        else if(currentWeaponVal.weaponName == "Fire Hammer")
        {
            Sword.SetActive(false);
            Hammer.SetActive(true);
            Whip.SetActive(false);

        } else if(currentWeaponVal.weaponName == "War Sword")
        {
            Sword.SetActive(true);
            Hammer.SetActive(false);
            Whip.SetActive(false);
        }
    }
}
