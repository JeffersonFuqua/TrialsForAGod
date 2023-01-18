using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIProj : MonoBehaviour
{
    private ProjEnemyValues enemyValuesProj;

    private GameObject player;
    public GameObject noticeObject;
    public Transform aimTool;

    public bool bNotice;
    //for exclimation mark
    private bool bNoticed;

    private Vector3 difference;
    [HideInInspector] public float enemySpeed;

    [HideInInspector] public bool bIsStunned;

    private bool bAttacking;

    //temp valiue
    float startPosY;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyValuesProj = GetComponent<ProjEnemyValueHolder>().projEnemyVal;
        enemySpeed = enemyValuesProj.enemySpeed;

        startPosY = transform.position.y;
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
                if (!bNoticed)
                {
                    StartCoroutine(noticeImage());
                    bNoticed = true;
                }
                bNotice = true;
                GetComponent<EnemyProjHealth>().PlaySound(enemyValuesProj.idleSound);
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            bNotice = false;
            bNoticed = false;
        }

        if (bNotice)
        {
            Vector3 backwardPos = transform.position - (player.transform.position - transform.position);
            backwardPos.y = startPosY;
            transform.position = Vector3.MoveTowards(transform.position, backwardPos, enemySpeed * Time.deltaTime);

            Vector3 lookVector = player.transform.position - transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        }
    }

    private void CallProj()
    {
        if ((Vector3.Distance(transform.position, player.transform.position) > 2 && bNotice) && !bIsStunned && !bAttacking)
        {
            StartCoroutine(projStartUp());
        }
    }
    
    IEnumerator projStartUp()
    {
        bAttacking = true;
        enemySpeed = 0;
        yield return new WaitForSeconds(enemyValuesProj.attackStartUp);
        if (!bIsStunned)
        {
            GetComponent<EnemyProjAttack>().EnemyProjSpecialAttack(player);
            StartCoroutine(attackEndLag());
        }
        StartCoroutine(attackCooldown());
    }
    IEnumerator attackEndLag()
    {
        yield return new WaitForSeconds(enemyValuesProj.attackEndLag);
        enemySpeed = enemyValuesProj.enemySpeed;
    }
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(enemyValuesProj.attackCooldown);
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
            difference = difference.normalized * 5;
            other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(15, difference);
        }
    }

    IEnumerator noticeImage()
    {
        noticeObject.SetActive(true);
        yield return new WaitForSeconds(1);
        noticeObject.SetActive(false);
    }
}
