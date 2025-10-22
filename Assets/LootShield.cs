using System.Collections;
using UnityEngine;

public class LootShield : MonoBehaviour
{
    private Shield shield;
    
    private void Start()
    {
        shield = FindObjectOfType<Shield>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            shield.ActivateShield(); 
            
            Destroy(gameObject); 
        }
    }
}