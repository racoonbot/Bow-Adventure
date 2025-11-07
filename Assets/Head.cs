using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Head : MonoBehaviour
{
    public EnemyHealth EnemyHealth;
    public TextMeshProUGUI headShot;

    private void Start()
    {
        headShot.enabled = false; 
    }

  

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<Bullet>() != null) 
        {
            ShowHeadShot(); 
            EnemyHealth.TakeDamage(2); 
            Destroy(other.gameObject); 
            
        }
    }

    public void ShowHeadShot()
    {
        StartCoroutine(ShowHeadShotUi()); // Запускаем корутину
    }

    private IEnumerator ShowHeadShotUi()
    {
        headShot.enabled = true; // Показываем текст
        yield return new WaitForSeconds(2f); // Ждем 2 секунды
        headShot.enabled = false; // Скрываем текст
    }

  
}
