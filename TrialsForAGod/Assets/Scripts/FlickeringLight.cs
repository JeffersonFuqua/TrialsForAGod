using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public bool bFlicker = false;
    public float timeDelay;
    public float minValue = 0.1f;
    public float maxValue = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bFlicker == false)
        {
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        bFlicker = true;
        this.gameObject.GetComponent<Light>().enabled = false; //Disables the light
        timeDelay = Random.Range(minValue, maxValue); //generates a random amount of time
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true; //Enables the light
        timeDelay = Random.Range(minValue, maxValue);
        yield return new WaitForSeconds(timeDelay);
        bFlicker = false; //causes the script to loop indefinitely, i belive
    }
}
