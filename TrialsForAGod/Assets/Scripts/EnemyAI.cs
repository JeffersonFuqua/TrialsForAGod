using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyValues enemyValues;

    public Transform aimTool;
    private GameObject player;
    public bool bChase;

    private Vector3 difference;
    [HideInInspector] public float enemySpeed;

    [HideInInspector] public bool bIsStunned;

    private bool bAttacking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyValues = GetComponent<EnemyValueHolder>().enemyValues;
        enemySpeed = enemyValues.enemySpeed;
    }

    private void FixedUpdate()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (!enemyValues.doesNotAttack && !bIsStunned)
        {
            Chase();
            CallAttack();
        }
    }

    private void Chase()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 6)
        {
            if (!bChase)
            {
                bChase = true;
                GetComponent<EnemyHealth>().PlaySound(enemyValues.idleSound);
            }
        }
        else if(Vector3.Distance(transform.position, player.transform.position) > 12)
        {
            bChase = false;
        }

        if (bChase && !bAttacking && !bIsStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z), enemySpeed * Time.deltaTime);
            Vector3 lookVector = transform.position - player.transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }
    }

    private void CallAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2 && !bAttacking && !bIsStunned)
        {
            StartCoroutine(attackStartUp());
        }
    }

    IEnumerator attackStartUp()
    {
        bAttacking = true;
        enemySpeed = 0;

        //enemy aim
        Vector3 lookVector = aimTool.position - player.transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        yield return new WaitForSeconds(enemyValues.attackStartUp);
        if (!bIsStunned)
        {
            GetComponent<EnemyAttack>().EnemySpecialAttack(player);
        }
        StartCoroutine(attackEndLag());
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
            //Debug.Log("touch");
            difference = player.transform.position - transform.position;
            difference.y = player.transform.position.y;
            difference = difference.normalized * enemyValues.attackKnockback;
            other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(enemyValues.attackDamage, difference);
        }
    }
}
