using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyValues enemyValues;

    public GameObject idleAttack;
    public GameObject attackRange;


    private void Start()
    {
        enemyValues = GetComponent<EnemyValueHolder>().enemyValues;
    }
    public void EnemySpecialAttack(GameObject player)
    {
        StartCoroutine(hitboxDuration());
    }

    IEnumerator hitboxDuration()
    {
        idleAttack.SetActive(false);
        attackRange.SetActive(true);
        GetComponent<EnemyHealth>().PlaySound(enemyValues.attackSound);
        yield return new WaitForSeconds(enemyValues.tempHitBoxDuration);
        attackRange.SetActive(false);
        idleAttack.SetActive(true);
    }
}
