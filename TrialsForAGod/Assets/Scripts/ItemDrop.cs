using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject commonDrop;
    public GameObject rareDrop;
    public Vector3 dropSpot;

    public int dropAmountLow, dropAmountHigh;
    private int dropAmount;

    [Range(0, 100)]
    public float rareDropRate;

    public void Start()
    {
        dropAmount = Random.Range(dropAmountLow, dropAmountHigh);
    }

    public void SetDrop(Vector3 deathSpot)
    {
        dropSpot.x = deathSpot.x + Random.Range(-0.2f, 0.2f);
        dropSpot.y = commonDrop.transform.position.y;
        dropSpot.z = deathSpot.z + Random.Range(-0.2f, 0.2f);
        Debug.Log("Set Drop");
    }

    public void DropItem(Vector3 deathSpot)
    {
        dropSpot.x = deathSpot.x + Random.Range(-0.2f, 0.2f);
        dropSpot.y = commonDrop.transform.position.y;
        dropSpot.z = deathSpot.z + Random.Range(-0.2f, 0.2f);

        for (int dropLoop = 0; dropLoop < dropAmount; dropLoop++)
        {
            Instantiate(commonDrop);
            //commonDrop.transform.position = this.transform.position;
            //  commonDrop.transform.position = new Vector3(commonDrop.transform.position.x + Random.Range(-0.2f, 0.2f), commonDrop.transform.position.y, commonDrop.transform.position.z + Random.Range(-0.1f, 0.1f));
            commonDrop.transform.position = dropSpot;

            Debug.Log("We be droppin");
            if (commonDrop != enabled)
                commonDrop.SetActive(true);
        }
        if(Random.Range(0, 100) < rareDropRate)
        {
            Instantiate(rareDrop);
           // rareDrop.transform.position = this.transform.position;
           // commonDrop.transform.position = new Vector3(commonDrop.transform.position.x + Random.Range(-0.2f, 0.2f), commonDrop.transform.position.y, commonDrop.transform.position.z + Random.Range(-0.2f, 0.2f));
            commonDrop.transform.position = dropSpot;
            if (rareDrop != enabled)
                rareDrop.SetActive(true);
            Debug.Log("Rare!");
        }
    }
}
