using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;

    public void AddHealth(int amount)
    {
        health += amount;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
