using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpeakerValues : ScriptableObject
{
    [Header("Reference")]
    public string speakerName;
    public Color speakerColor;

    [Header("Images")]
    public Texture angry, flirty, happy, neutral, dismissive;
}
