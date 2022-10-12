using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public GameObject menu;
    public PlayerValues pVal;
    public GameObject pName;
    public Button bStart;

    private void Start()
    {
        pName.GetComponent<TextMeshProUGUI>().text = pVal.playerName;
        bStart.onClick.AddListener(SetName);
    }
    private void Update()
    {
        pVal.playerName = pName.GetComponent<TextMeshProUGUI>().text;
    }
    public void SetName()
    {
        if(pVal.playerName.Length <= 1)
        {
            Debug.Log("name isn't put in");
        }
        else
        {
            menu.GetComponent<MainMenuManager>().StartGame();
        }
    }
}