using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyValues : ScriptableObject
{
    [Header("Enemy Info")]
    public string enemyName;
    public Material baseMaterial;

    [Header("Enemy Base Stats")]
    public float enemyMaxHealth;
    public float enemySpeed;

    [Header("Enemy Attack Stats")]
    public float attackDamage;
    public float attackStartUp;
    public float attackEndLag;
    public float attackCooldown;
    public float attackKnockback;
    public float tempHitBoxDuration;
    public bool doesNotAttack;
    public bool hasDeathAttack;

    [Header("Enemy Sounds")]
    public AudioClip idleSound;
    public AudioClip attackSound;
    public List<AudioClip> tookDamageSound = new List<AudioClip>();
    public AudioClip deathSound;

}
