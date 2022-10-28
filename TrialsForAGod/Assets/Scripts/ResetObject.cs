using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObject : MonoBehaviour
{
    [HideInInspector]public Vector3 resetPosition;
    [HideInInspector]public bool bUponReset;

    private void Start()
    {
        resetPosition = transform.position;
    }

    private void OnEnable()
    {
        ResetDelegate.Reset += ActiveReset;
    }

    private void OnDisable()
    {
        ResetDelegate.Reset -= ActiveReset;
    }

    private void ActiveReset()
    {
        transform.position = resetPosition;
        bUponReset = true;
        if (TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth) && TryGetComponent<PlayerValueHolder>(out PlayerValueHolder maxHealth))
        {
            GetComponent<PlayerHealth>().currentHealth = GetComponent<PlayerValueHolder>().playerVal.playerMaxHealth;
            GetComponent<PlayerHealth>().playerHealthBar.value = GetComponent<PlayerHealth>().currentHealth;
        }
        if (TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth) && TryGetComponent<EnemyValueHolder>(out EnemyValueHolder enemyMaxHealth))
        {
            GetComponent<EnemyHealth>().currentHealth = GetComponent<EnemyValueHolder>().enemyValues.enemyMaxHealth;
            GetComponent<EnemyAI>().bChase = false;
        }
        StartCoroutine(resetTimer());
    }

    IEnumerator resetTimer()
    {
        yield return new WaitForSeconds(1);
        bUponReset = false;
    }
}
