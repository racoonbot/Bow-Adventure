using System;

using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 2f ;
    public float currentHealth;
    public LevelManager levelManager;
    public ParticleSystem particle;
    bool isDead;
    

    public event Action OnDeathEnemy;


    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            AddEnemyMaxHeath();
            currentHealth = maxHealth;
            isDead = false;
            
        }
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
        Instantiate(particle, transform.position, Quaternion.identity);
        Debug.Log(gameObject.name + " is dead");
        Destroy(gameObject);
    }

    private void AddEnemyMaxHeath()
    {
        maxHealth = levelManager.level + 1;
    }

}