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
            var player = FindObjectOfType<PlayerHealth>();
            if (player != null)
            {
                player.AddHealth();
                Debug.Log("Здоровье игрока увеличено!");
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}