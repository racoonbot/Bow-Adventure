using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 2f;
    public float currentHealth;

    public event Action OnDeathEnemy;

    bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            DieAndNotify();
        }
    }
    void DieAndNotify()
    {
        if (isDead) return;
        isDead = true;
        OnDeathEnemy?.Invoke();
        Debug.Log("EnemyDeath");
        Destroy(gameObject);
    }

}