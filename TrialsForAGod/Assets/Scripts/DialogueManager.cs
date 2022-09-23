using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    //replaces all /name with the player name in the player info
    public PlayerValues pName;
    [HideInInspector]
    public StringBuilder sb = new StringBuilder();

    public GameObject dialogueCanvas;

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;

    //speaker value
    [HideInInspector]
    public int iName;
    private int jSent;
    private bool bHasChoice;
    [HideInInspector]
    public int afterChoiceVal;

    [Range(1, 10)]
    public float typeSpeed;
    private float typeStart;
    bool bIsTalking;

    private void Start()
    {
        typeSpeed = 11 - typeSpeed;
        typeSpeed /= 100;
        typeStart = typeSpeed;
    }

    public void StartDialogue(DialogueSystem dialogue)
    {
        //fills sb with the name and replaces it with the saved player name
        sb.Append(dialogue.conversation[iName].charName);
        sb.Replace("/name", pName.playerName);

        dialogueCanvas.SetActive(true);

        if (dialogue.conversation[iName].choice)
        {
            bHasChoice = true;
        }
        //changes visual text in speech to what is in the sb
        nameText.text = sb.ToString();
        DisplayNextSentance(dialogue);
    }

    public void DisplayNextSentance(DialogueSystem dialogue)
    {
        //StopAllCoroutines(); 
        if(dialogue.conversation[iName].choicePath == afterChoiceVal || dialogue.conversation[iName].choicePath == 0)
        {
            if (jSent < dialogue.conversation[iName].sentences.Length)
            {
                if (bIsTalking)
                {
                    SkipLine(dialogue);
                }
                else
                {
                    EmotionImageSwap(dialogue);
                    typeSpeed = typeStart;
                    StartCoroutine(ReadLine(dialogue));
                }
            }
            else if (jSent == dialogue.conversation[iName].sentences.Length)
            {
                if (iName == dialogue.conversation.Count - 1)
                {
                    EndDialogue();
                    return;
                }
                iName++;
                jSent = 0;

                if (bHasChoice)
                {
                    bHasChoice = false;
                    GetComponent<DialogueChoice>().nextButton.SetActive(false);
                    //checks how many choices before moving on
                    GetComponent<DialogueChoice>().Choice(dialogue.conversation[iName - 1].choices.Length, dialogue.conversation[iName - 1].choices, dialogue);
                }
                else
                {
                    sb.Clear();
                    StartDialogue(dialogue);
                }

            }
        }
        else if(dialogue.conversation[iName].choicePath != afterChoiceVal && dialogue.conversation[iName].choicePath != 0)
        {
            if (iName == dialogue.conversation.Count - 1)
            {
                EndDialogue();
                return;
            }
            iName++;
            jSent = 0;
            sb.Clear();
            StartDialogue(dialogue);
        }
        
        
    }

    private void SkipLine(DialogueSystem dialogue)
    {
        StopAllCoroutines();
        sb.Clear();
        sb.Append(dialogue.conversation[iName].sentences[jSent]);
        sb.Replace("/name", pName.playerName);

        dialogueText.text = sb.ToString();
        bIsTalking = false;
        jSent++;
    }
    IEnumerator ReadLine(DialogueSystem dialogue)
    {
        bIsTalking = true;
        dialogueText.text = "";
        sb.Clear();

        sb.Append(dialogue.conversation[iName].sentences[jSent]);
        sb.Replace("/name", pName.playerName);
        foreach (char letter in sb.ToString().ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        bIsTalking = false;
        jSent++;
    }

    private void EmotionImageSwap(DialogueSystem dialogue)
    {
        //for EVAN ;)
        if (dialogue.conversation[iName].emotion == Dialogue.EmotionState.happy)
        {
            Debug.Log("happy");
        }
    }


    public void EndDialogue()
    {
        Debug.Log("End");
        dialogueCanvas.SetActive(false);
    }
}
