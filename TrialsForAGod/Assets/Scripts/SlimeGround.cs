using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGround : MonoBehaviour
{
    public float idleDamage = 4;
    public float idleTime = 5;

    public void Start()
    {
        StartCoroutine(endPuddle());
    }

    private void OnTriggerStay(Collider other)
    {
        //uncomment to make enemy not attack through walls
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, 2, wallLayer);

        if (other.CompareTag("Player"))
        {
            //Debug.Log("touch");
            //difference = player.transform.position - transform.position;
            //difference.y = player.transform.position.y;
            //difference = difference.normalized * enemyValues.attackKnockback;
            other.GetComponent<PlayerHealth>().TakeDamageAndKnockback(idleDamage, Vector3.zero, true);
        }
    }

    IEnumerator endPuddle()
    {
        yield return new WaitForSeconds(idleTime);
        Destroy(gameObject);
    }
}
