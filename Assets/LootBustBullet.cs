using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBustBullet : MonoBehaviour
{
    private Player player;
    private BuffTimer buffTimer;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        buffTimer  = FindObjectOfType<BuffTimer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            Debug.Log("Hit bullet");
            player.isBuffed = true;
            buffTimer.StartBuffTimer();
            Destroy(gameObject);
        }
    }
}