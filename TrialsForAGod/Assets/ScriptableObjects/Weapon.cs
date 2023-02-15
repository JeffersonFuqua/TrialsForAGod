using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;

    [Header("Weapon Light Attack Stats")]
    public float lightDamage1;
    public float lightKnockback1;
    public float lightCooldown1;
    public float lightDamage2;
    public float lightKnockback2;
    public float lightCooldown2;
    public float lightDamage3;
    public float lightKnockback3;
    public float lightCooldown3;
    public float lightStartUp;

    [Header("Weapon Heavy Attack Stats")]
    public float heavyDamage1;
    public float heavyKnockback1;
    public float heavyCooldown1;
    public float heavyDamage2;
    public float heavyKnockback2;
    public float heavyCooldown2;
    public float heavyDamage3;
    public float heavyKnockback3;
    public float heavyCooldown3;
    public float heavyStartUp;

    [Header("Weopon Sounds")]
    public List<AudioClip> lightSound1;
    public List<AudioClip> lightSound2;
    public List<AudioClip> lightSound3;
    public List<AudioClip> heavySound1;
    public List<AudioClip> heavySound2;
    public List<AudioClip> heavySound3;
}
