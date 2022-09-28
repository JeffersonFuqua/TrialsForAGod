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
        CallAttack();
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
        enemySpeed = 0;
        yield return new WaitForSeconds(enemyValues.attackStartUp);
        if (!bIsStunned)
        {
            GetComponent<EnemyAttack>().EnemySpecialAttack(player);
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

    private void OnTriggerStay(Collider other)
    {
        //uncomment to make enemy not attack through walls
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, 2, wallLayer);

        if (other.CompareTag("Player"))
        {
            Debug.Log("touch");
            difference = other.transform.position - transform.position;
            if (bAttacking)
            {
                difference = difference.normalized * enemyValues.attackKnockback;
                other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(enemyValues.attackDamage, difference);
            }
            else
            {
                difference = difference.normalized * enemyValues.attackKnockback;
                other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(enemyValues.attackDamage, difference);
            }
        }
    }
}
