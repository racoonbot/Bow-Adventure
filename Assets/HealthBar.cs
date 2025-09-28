using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private  Image playerHealthBar;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealthBar =  GameObject.Find("PlayerHealthBar").GetComponent<Image>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        playerHealthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
    }
}