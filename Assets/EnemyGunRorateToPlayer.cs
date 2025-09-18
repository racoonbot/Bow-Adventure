using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotateToPlayer : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    {
        RotateTowardsPlayer();
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.right = direction; //!!! 
        }
    }
}