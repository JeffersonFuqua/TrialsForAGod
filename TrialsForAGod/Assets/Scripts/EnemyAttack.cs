using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyValues enemyVal;

    public GameObject idleAttack;
    public GameObject attackRange;


    private void Start()
    {
        enemyVal = GetComponent<EnemyValueHolder>().enemyVal;
    }
    public void EnemySpecialAttack(GameObject player)
    {
        StartCoroutine(hitboxDuration());
    }

    IEnumerator hitboxDuration()
    {
        idleAttack.SetActive(false);
        attackRange.SetActive(true);
        yield return new WaitForSeconds(enemyVal.tempHitBoxDuration);
        attackRange.SetActive(false);
        idleAttack.SetActive(true);
    }
}
