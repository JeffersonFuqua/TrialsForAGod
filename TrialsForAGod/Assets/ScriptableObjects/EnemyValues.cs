using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyValues : ScriptableObject
{
    [Header("Enemy Info")]
    public string enemyName;

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
    
}
