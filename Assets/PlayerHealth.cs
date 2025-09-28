using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public Action OnTouched;

    private void OnEnable()
    {
        OnTouched += TakeDamage;
    }

    private void OnDisable()
    {
        OnTouched -= TakeDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            OnTouched?.Invoke();
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 1;
        Debug.Log($"Получили урон. Здоровье: {currentHealth}");
    }

    public void AddHealth()
    {
        currentHealth += 1;
        Debug.Log($"Добавлено здоровье. Здоровье: {currentHealth}");
    }
    
}