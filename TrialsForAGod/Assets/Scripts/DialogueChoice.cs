using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueChoice : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject choiceButton;
    public GameObject ES;

    public Transform mainCanvas;

    private GameObject madeButtons;
    private int numberofButtons;

    private int i;
    private int j = 1;

    public void Choice(int c, string[] choices, DialogueSystem dialogue)
    {
        numberofButtons = choices.Length;
        for(i = 0; i < choices.Length; i++)
        {
            madeButtons = Instantiate(choiceButton, mainCanvas);
            madeButtons.transform.SetParent(this.transform);
            ButtonsValue bv = madeButtons.GetComponent<ButtonsValue>();
            bv.dChoice = this;
            bv.index = j;
            madeButtons.GetComponent<Transform>().position += new Vector3(400, (i * 60) - 165, -1);
            madeButtons.GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            madeButtons.GetComponent<Button>().onClick.AddListener(() => bv.TriggerChoice());
            madeButtons.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<DialogueManager>().StartDialogue(dialogue); });
            j++;
            ES.GetComponent<EventSystem>().SetSelectedGameObject(madeButtons, null);

        }
        /*
        foreach(string choiceName in choices)
        {
            GameObject madeButtons = Instantiate(choiceButton, mainCanvas);
            madeButtons.GetComponent<Transform>().position += new Vector3(0, i * 40, 0);
            madeButtons.GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
            madeButtons.GetComponent<Button>().onClick.AddListener(() => AfterChoice(j));
            madeButtons.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<DialogueManager>().StartDialogue(dialogue);});
            j++;
            i++;
        }
        */
    }

    public void AfterChoice(int d)
    {
        Debug.Log("AfterChoice: " + d);
        //button press has value that the conversation will now only show the value or 0
        GetComponent<DialogueManager>().afterChoiceVal = d;
        GetComponent<DialogueManager>().sb.Clear();
        nextButton.SetActive(true);
        
        for(int i = 0; i < numberofButtons; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        ES.GetComponent<EventSystem>().SetSelectedGameObject(nextButton, null);
    }
}
