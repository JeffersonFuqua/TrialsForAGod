using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueSystem : MonoBehaviour
{
    public List<Dialogue> conversation;
}

[System.Serializable]
public struct Dialogue
{
    public string charName;
    public enum EmotionState { neutral, happy, sad, angry, sexy };
    public EmotionState emotion;
    [TextArea(3, 10)]
    public string[] sentences;
    public bool choice;
    public int choicePath;
    public string[] choices;
}