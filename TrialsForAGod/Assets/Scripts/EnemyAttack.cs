using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyValues enemyVal;

    public Transform aimTool;
    private Vector3 aimDirection;
    public GameObject idleAttack;
    public GameObject attackRange;


    private void Start()
    {
        enemyVal = GetComponent<EnemyValueHolder>().enemyVal;
    }
    public void EnemySpecialAttack(GameObject player)
    {
        Vector3 lookVector = aimTool.position - player.transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
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
