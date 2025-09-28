using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 2;
    public float currentHealth;
    
   public Action OnDeathEnemy;

   private void OnEnable()
   {
       OnDeathEnemy += Die;
   }

   private void OnDisable()
   {
       OnDeathEnemy -= Die;
   }

   private void Start()
    {
        currentHealth = maxHealth;
   
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        OnDeathEnemy?.Invoke();
        Debug.Log("EnemyDeath");
    }
}