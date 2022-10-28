using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    //replaces all /name with the player name in the player info
    public PlayerValues pName;
    public DialogueSystem dSystem;
    [HideInInspector]
    public StringBuilder sb = new StringBuilder();

    public GameObject dialogueCanvas;

    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public SpeakerValues speakerOneSO, speakerTwoSo;
    public EventSystem ES;

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
    public RawImage leftPortrait, middlePortrait, rightPortrait, bgImage;

    public GameObject skipButton, nextButton;
    PlayerActions pActions;

    private void Start()
    {
        pActions = new PlayerActions();
        pActions.Enable();

        pActions.PlayerControls.NextLine.performed += HitSpace;

        typeSpeed = 11 - typeSpeed;
        typeSpeed /= 100;
        typeStart = typeSpeed;
        leftPortrait.enabled = false;
        middlePortrait.enabled = false;
        rightPortrait.enabled = false;
        StartDialogue(dSystem);
    }

    private void OnDisable()
    {
        pActions.Disable();
        pActions.PlayerControls.NextLine.performed -= HitSpace;
    }

    public void StartDialogue(DialogueSystem dialogue)
    {
        //fills sb with the name and replaces it with the saved player name
        if(dialogue.conversation[iName].charName == "")
        {
            nameText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            nameText.transform.parent.gameObject.SetActive(true);
        }
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
                    BackgroundSwap(dialogue);
                    typeSpeed = typeStart;
                    StartCoroutine(ReadLine(dialogue));
                }
            }
            else if (jSent == dialogue.conversation[iName].sentences.Length)
            {
                if (iName == dialogue.conversation.Count - 1)
                {
                    EndDialogue(dialogue);
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
                EndDialogue(dialogue);
                return;
            }
            iName++;
            jSent = 0;
            sb.Clear();
            StartDialogue(dialogue);
        }
        if(iName != 0)
        {
            if (dialogue.conversation[iName].choice || dialogue.conversation[iName - 1].choice)
            {
                skipButton.SetActive(false);
            }
            else
            {
                skipButton.SetActive(true);
            }
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

    private void BackgroundSwap(DialogueSystem dialogue)
    {
        bgImage.texture = dialogue.conversation[iName].background;
    }

    private void EmotionImageSwap(DialogueSystem dialogue)
    {
            switch(dialogue.conversation[iName].speakerCount)
            {
            case 0:
                leftPortrait.enabled = false;
                middlePortrait.enabled = false;
                rightPortrait.enabled = false;
                break;
            case 1:
                leftPortrait.enabled = false;
                middlePortrait.enabled = true;
                rightPortrait.enabled = false;
                break;
            case 2:
                leftPortrait.enabled = true;
                middlePortrait.enabled = false;
                rightPortrait.enabled = true;
                break;
            default:
                leftPortrait.enabled = false;
                middlePortrait.enabled = false;
                rightPortrait.enabled = false;
                break;
            }
        if (dialogue.conversation[iName].emotionOne == Dialogue.EmotionState.happy)
        {
            if(dialogue.conversation[iName].speakerCount == 1)
                middlePortrait.texture = speakerOneSO.happy;
            if (dialogue.conversation[iName].speakerCount == 2)
                leftPortrait.texture = speakerOneSO.happy;
        }
        else if (dialogue.conversation[iName].emotionOne == Dialogue.EmotionState.angry)
        {
            if (dialogue.conversation[iName].speakerCount == 1)
                middlePortrait.texture = speakerOneSO.angry;
            if (dialogue.conversation[iName].speakerCount == 2)
                leftPortrait.texture = speakerOneSO.angry;
        }
        else if (dialogue.conversation[iName].emotionOne == Dialogue.EmotionState.dismissive)
        {
            if (dialogue.conversation[iName].speakerCount == 1)
                middlePortrait.texture = speakerOneSO.dismissive;
            if (dialogue.conversation[iName].speakerCount == 2)
                leftPortrait.texture = speakerOneSO.dismissive;
        }
        else if (dialogue.conversation[iName].emotionOne == Dialogue.EmotionState.neutral)
        {
            if (dialogue.conversation[iName].speakerCount == 1)
                middlePortrait.texture = speakerOneSO.neutral;
            if (dialogue.conversation[iName].speakerCount == 2)
                leftPortrait.texture = speakerOneSO.neutral;
        }
        else if (dialogue.conversation[iName].emotionOne == Dialogue.EmotionState.flirty)
        {
            if (dialogue.conversation[iName].speakerCount == 1)
                middlePortrait.texture = speakerOneSO.flirty;
            if (dialogue.conversation[iName].speakerCount == 2)
                leftPortrait.texture = speakerOneSO.flirty;
        }
        if(dialogue.conversation[iName].speakerCount == 2)
        {
            if (dialogue.conversation[iName].emotionTwo == Dialogue.EmotionState.happy)
            {
                rightPortrait.texture = speakerTwoSo.happy;
            }
            else if (dialogue.conversation[iName].emotionTwo == Dialogue.EmotionState.angry)
            {
                rightPortrait.texture = speakerTwoSo.angry;
            }
            else if (dialogue.conversation[iName].emotionTwo == Dialogue.EmotionState.dismissive)
            {
                rightPortrait.texture = speakerTwoSo.dismissive;
            }
            else if (dialogue.conversation[iName].emotionTwo == Dialogue.EmotionState.neutral)
            {
                rightPortrait.texture = speakerTwoSo.neutral;
            }
            else if (dialogue.conversation[iName].emotionTwo == Dialogue.EmotionState.flirty)
            {
                rightPortrait.texture = speakerTwoSo.flirty;
            }
        }

    }

    public void SkipDialogue(DialogueSystem dialogue)
    {
        for(int i = iName; i < dialogue.conversation.Count; i++)
        {
            if (dialogue.conversation[i].choice)
            {
                //stops any current line reading and clears the sb
                StopAllCoroutines();
                sb.Clear();
                //loads dialogue that has a choice in it NOTE: loads from the first sentence in string not right at options
                iName = i;
                jSent = 0;

                //loads new dialogue scene
                StartDialogue(dialogue);
                EmotionImageSwap(dialogue);
                BackgroundSwap(dialogue);
                ES.GetComponent<EventSystem>().SetSelectedGameObject(nextButton, null);
                return;
            }

            if(i == dialogue.conversation.Count - 1)
            {
                GetComponent<NextScene>().ChangeScene();
            }
        }
    }

    private void HitSpace(InputAction.CallbackContext c)
    {
        if(nextButton.active == true)
        DisplayNextSentance(dSystem);
    }
    public void EndDialogue(DialogueSystem dialogue)
    {
        if (dialogue.conversation[iName].bNextScene)
        {
            GetComponent<NextScene>().ChangeScene();
        }
        Debug.Log("End");
        dialogueCanvas.SetActive(false);
    }
}
