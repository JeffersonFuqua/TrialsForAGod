using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{
    [Header ("Player Info")]
    public string playerName;
    public Color playerColor;
    public Material playerBaseMaterial;

    [Header("Player Base Stats")]
    public float playerMaxHealth;
    public float playerSpeed;

    [Header("Player Dodge Stats")]
    public float dodgeDuration;
    public float dodgeSpeed;
    public float dodgeCooldown;

    [Header("Misc.")]
    public AudioClip runningSFX;

}
