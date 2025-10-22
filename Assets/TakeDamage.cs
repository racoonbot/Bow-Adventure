using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject enemy;
    private Enemy enemyComponent;

    private void Start()
    {
        enemy.SetActive(true);
        enemyComponent = enemy.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            Debug.Log("OnTriggerEnter");
            
            enemyComponent.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
