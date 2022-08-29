using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;

    [Header("Weapon Light Attack Stats")]
    public float lightAttackDamage1;
    public float lightAttackDamage2;
    public float lightAttackDamage3;
    public float lightAttackStartUp;
    public float lightAttackCooldown;

    [Header("Weapon Heavy Attack Stats")]
    public float heavyAttackDamage1;
    public float heavyAttackDamage2;
    public float heavyAttackDamage3;
    public float heavyAttackStartUp;
    public float heavyAttackCooldown;

}
