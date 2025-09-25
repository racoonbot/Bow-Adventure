using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            
        }
    }
}
