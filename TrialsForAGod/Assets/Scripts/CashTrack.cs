using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CashTrack : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;
    private static float coins;
    private float savedCoins;

    private void OnEnable()
    {
        PickUps.UpdateCash += UpdateCoins;
        if(SceneManager.GetActiveScene().name == "Sewer Tutorial")
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            coins = 0;
            savedCoins = 0;
        }
        coinDisplay.text = coins.ToString();
    }
    private void OnDisable()
    {
        PickUps.UpdateCash -= UpdateCoins;
        coins = savedCoins;
    }
    
    public void UpdateCoins(float cashflow)
    {
        coins += cashflow;
        coinDisplay.text = coins.ToString();
        savedCoins = coins;
    }
}