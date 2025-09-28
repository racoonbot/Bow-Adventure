using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private  Image enemyHealthBar;
    [SerializeField] private EnemyHealth enemyHealth;

    private void Start()
    {
        
        enemyHealthBar =  GameObject.Find("EnemyHealthBar").GetComponent<Image>();
        enemyHealth = GameObject.Find("EnemyHealth").GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        enemyHealthBar.fillAmount = enemyHealth.currentHealth / enemyHealth.maxHealth;
    }
}