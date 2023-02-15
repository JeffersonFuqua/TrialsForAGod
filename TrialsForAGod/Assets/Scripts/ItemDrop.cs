using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject commonDrop;
    public GameObject rareDrop;

    public int dropAmountLow, dropAmountHigh;

    [Range (0, 100)]
    public float rareDropRate;
    public void DropItem()
    {
        Debug.Log("We be droppin");
        for (int dropLoop = 0; dropLoop < Random.Range(dropAmountLow, dropAmountHigh); dropLoop++)
        {
            Instantiate(commonDrop);
            commonDrop.transform.position = this.transform.position;
            commonDrop.transform.position = new Vector3(commonDrop.transform.position.x + Random.Range(-0.5f, 0.5f), commonDrop.transform.position.y, commonDrop.transform.position.z + Random.Range(-0.5f, 0.5f));
            Debug.Log("Commoon...");
            if (commonDrop != enabled)
                commonDrop.SetActive(true);
        }
        if(Random.Range(0, 100) < rareDropRate)
        {
            Instantiate(rareDrop);
            rareDrop.transform.position = this.transform.position;
            if (rareDrop != enabled)
                rareDrop.SetActive(true);
            Debug.Log("Rare!");
        }
    }
}
