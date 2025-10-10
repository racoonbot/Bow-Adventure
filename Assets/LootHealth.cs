using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            var player = FindObjectOfType<PlayerHealth>(); // Найдите игрока на сцене
            if (player != null)
            {
                player.AddHealth(); 
                Debug.Log("Здоровье игрока увеличено!");
            }

            // Опционально: уничтожьте пуль или объект Sphere
            Destroy(other.gameObject); // Уничтожаем пулю
            Destroy(gameObject); // Уничтожаем объект Sphere
        }
    }
}