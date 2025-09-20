using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunRotateToPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public Transform bulletSpawn;
    private float shootTimer;

    public Vector3 direction;
    public float force;
    public Transform bow;

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
        Rigidbody rbBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
    
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude; // Вычисляем расстояние до игрока
        // direction.y = 0; // Игнорируем вертикальное расстояние для начального направления
        direction.Normalize(); // Нормализуем вектор направления

        // Разница в высоте
        float heightDifference = player.transform.position.y - transform.position.y;
        float gravity = Mathf.Abs(Physics.gravity.y);

        // Вычисляем угол стрельбы
        float angle = Mathf.Atan((Mathf.Pow(distance, 2) * gravity) / (2 * (heightDifference * distance + Mathf.Pow(distance, 2) * gravity))) * Mathf.Rad2Deg;

        // Проверяем, что угол не выходит за пределы
        if (angle < 0 || angle > 90)
        {
            angle = 45; // Угол по умолчанию, если расчет неудачен
        }

        // Вычисляем необходимую скорость
        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle * Mathf.Deg2Rad));

        // Определяем вектор скорости
        Vector3 shootDirection = direction * velocity;
        shootDirection.y = velocity * Mathf.Sin(angle * Mathf.Deg2Rad); // Добавляем вертикальную составляющую

        // Применяем силу к снаряду
        rbBullet.AddForce(shootDirection, ForceMode.VelocityChange);
    }


    public void GetShootTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100]; //!
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2;
        }
    }
}