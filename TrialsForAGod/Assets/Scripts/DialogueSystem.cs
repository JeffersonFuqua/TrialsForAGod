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
    public Color charColor;
    public enum EmotionState { neutral, happy, dismissive, angry, flirty };
    public EmotionState emotionOne, emotionTwo;
    [TextArea(5, 10)]
    public string[] sentences;
    public bool choice;
    public int choicePath;
    public string[] choices;
    [Range(0, 2)]
    public int speakerCount;
    public Texture background;
    public bool bNextScene;
}