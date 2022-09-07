using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsValue : MonoBehaviour
{
    public int index;
    public DialogueChoice dChoice;

    public void TriggerChoice()
    {
        dChoice.AfterChoice(index);
    }
}
