using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashTrack : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;
    private float coins;

    private void Start()
    {
        coins = 0; //Will need to set it to another amount stored in the player SO
    }
    private void OnEnable()
    {
        PickUps.UpdateCash += UpdateCoins;
    }
    private void OnDisable()
    {
        PickUps.UpdateCash -= UpdateCoins;
    }
    public void UpdateCoins(float cashflow)
    {
        coins += cashflow;
        coinDisplay.text = coins.ToString();
    }
}