using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    public PlayerValues pVal;
    public GameObject pName;

    private void Update()
    {
        pVal.playerName = pName.GetComponent<TextMeshProUGUI>().text;
    }

}
