using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjEnemyValues : ScriptableObject
{
    [Header("Enemy Info")]
    public string enemyNameProj;

    [Header("Enemy Base Stats")]
    public float enemyMaxHealth;
    public float enemySpeed;

    [Header("Enemy Attack Stats")]
    public GameObject projectile;
    public float attackStartUp;
    public float attackEndLag;
    public float attackCooldown;

    [Header("Enemy Sounds")]
    public AudioClip idleSound;
    public AudioClip attackSound;
    public AudioClip tookDamageSound;
    public AudioClip deathSound;
}
