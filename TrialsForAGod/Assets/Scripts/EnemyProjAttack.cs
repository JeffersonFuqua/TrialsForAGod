using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjAttack : MonoBehaviour
{
    private ProjEnemyValues projEnemyVal;

    private void Start()
    {
        projEnemyVal = GetComponent<ProjEnemyValueHolder>().projEnemyVal;
    }

    public void EnemyProjSpecialAttack(GameObject player)
    {
        GameObject proj = Instantiate(projEnemyVal.projectile);
        proj.transform.position = transform.position;
        Rigidbody rbProj = proj.GetComponent<Rigidbody>();
        rbProj.AddForce(transform.forward * projEnemyVal.enemySpeed, ForceMode.Impulse);
    }
}
