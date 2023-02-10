using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemToDrop;

    [Range (0, 100)]
    public float dropRate;

    public void OnEnable()
    {
        EnemyHealth.DropItem += DropItem;
    }

    public void OnDisable()
    {
        EnemyHealth.DropItem -= DropItem;
    }

    public void DropItem()
    {
        Instantiate(itemToDrop);
        itemToDrop.transform.position = this.transform.position;
        Debug.Log("Drop");
    }
}
