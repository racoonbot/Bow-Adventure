using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotateToPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public Transform bulletSpawn;
    private float shootTimer;

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= 2f)
        {
            EnemyShoot();
            shootTimer = 0f;
        }
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.right = direction; //!!! 
        }
    }

    void EnemyShoot()
    {
        RotateTowardsPlayer();
        Rigidbody rbBullet = Instantiate(
            bullet, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 direction = player.transform.position - bulletSpawn.position;
        rbBullet.AddForce(direction * 200);
    }
}