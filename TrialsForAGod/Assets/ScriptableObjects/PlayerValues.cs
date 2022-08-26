using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{
    [Header ("Player Info")]
    public string playerName;

    [Header("Player Base Stats")]
    public float maxHealth;

    [Header("Player Invincibility")]
    public float invincibilityTime;
    public float dodgeSpeed;

}
