using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySound : ScriptableObject
{
    [Header("Enemy Sounds")]
    public AudioClip idleSound;
    public AudioClip attackSound;
    public AudioClip tookDamage;
    public AudioClip deathSound;
}
