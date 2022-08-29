using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamage : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    public int Damage => -damage;
}
