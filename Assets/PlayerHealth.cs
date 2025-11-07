using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public float currentHealth;
    public HealScreen healScreen;

    private void Start()
    {
        healScreen = FindObjectOfType<HealScreen>();
        healScreen.gameObject.SetActive(false);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public event Action OnTouched;
    public event Action OnDead;

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
    }

    public void AddHealth()
    {
        currentHealth += 1;
        StartCoroutine(AddHealthScreen());
    }

    public void Die()
    {
        OnDead?.Invoke(); ///!! 
        gameObject.SetActive(false);
    }

    private IEnumerator AddHealthScreen()
    {
        healScreen.gameObject.SetActive(true);
        Image image = healScreen.GetComponent<Image>();
        Color color = image.color;
        color.a = 1; 
        image.color = color; 

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
 
            color.a = Mathf.Lerp(1, 0, t);
            image.color = color;
            yield return null; 
        }
        color.a = 0; 
        image.color = color;
        healScreen.gameObject.SetActive(false);
    }
}
