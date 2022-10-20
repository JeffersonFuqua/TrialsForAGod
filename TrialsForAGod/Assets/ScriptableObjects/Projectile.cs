using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Projectile : ScriptableObject
{
    [Header("Projectile Values")]
    public float projDamage;
    public float projKnocback;
    public float projSpeed;
}
