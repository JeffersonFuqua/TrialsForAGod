using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyValues enemyValues;

    private GameObject player;
    private bool bChase;

    private Vector3 difference;
    [HideInInspector] public float enemySpeed;

    private bool bIsStunned;

    private bool bAttacking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyValues = GetComponent<EnemyValueHolder>().enemyVal;
        enemySpeed = enemyValues.enemySpeed;
    }

    private void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            if (!bChase)
            {
                bChase = true;
            }
        }

        if (bChase)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }
    }

    private void CallAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3 && !bAttacking && !bIsStunned)
        {
            StartCoroutine(attackStartUp());
        }
    }

    IEnumerator attackStartUp()
    {
        bAttacking = true;
        GetComponent<SpriteRenderer>().color = Color.black;
        enemySpeed = 0;
        yield return new WaitForSeconds(enemyValues.attackStartUp);
        if (!bIsStunned)
        {
            //GetComponent<EnemyAttack>().EnemySpecialAttack(player);
            StartCoroutine(attackEndLag());
        }
        StartCoroutine(attackCooldown());
    }
    IEnumerator attackEndLag()
    {
        yield return new WaitForSeconds(enemyValues.attackEndLag);
        enemySpeed = enemyValues.enemySpeed;
    }
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(enemyValues.attackCooldown);
        bAttacking = false;
    }
}
