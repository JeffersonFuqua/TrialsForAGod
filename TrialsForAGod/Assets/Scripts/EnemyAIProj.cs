using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIProj : MonoBehaviour
{
    private EnemyValues enemyValues;

    private GameObject player;



    public bool bNotice;

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
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        WalkBack();
        CallProj();
    }

    private void WalkBack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 7)
        {
            if (!bNotice)
            {
                bNotice = true;
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 12)
        {
            bNotice = false;
        }

        if (bNotice)
        {
            Vector3 backwardPos = transform.position - (player.transform.position - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, backwardPos, enemySpeed * Time.deltaTime);
            
            Vector3 lookVector = transform.position - player.transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }
    }

    private void CallProj()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) < 3 && Vector3.Distance(transform.position, player.transform.position) < 3) && !bIsStunned)
        {
            //StartCoroutine(attackStartUp());
        }
    }
    /*
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
    */
}
