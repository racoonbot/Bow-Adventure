using System;

using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 2f;
    public float currentHealth;
    bool isDead;
    

    public event Action OnDeathEnemy;


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
        Debug.Log(gameObject.name + " is dead");
        Destroy(gameObject);
    }

}