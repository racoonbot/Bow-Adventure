using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public Transform SpawnTransform; // Точка спавна пули
    private Transform TargetTransform; // Цель, к которой будет стрелять
    public GameObject BulletPrefab; // Префаб пули
    public float AngleInDegrees = 30f; // Угол стрельбы в градусах
    public float Timer = 2f; // Таймер между выстрелами
    private float shotTimer; // Таймер для отслеживания времени между выстрелами
    public Vector3 shootDir; // Направление стрельбы
    public Transform bow; // лук 

    void Start()
    {
        TargetTransform = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        // Проверяем, что точки спавна и цели заданы
        if (SpawnTransform == null || TargetTransform == null) return;

        shotTimer += Time.deltaTime; // Увеличиваем таймер выстрела

        // Вычисляем направление к цели
        Vector3 dir = TargetTransform.position - SpawnTransform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z); // Игнорируем вертикальную составляющую
        if (dirXZ.sqrMagnitude > 0.0001f) // Проверяем, что направление не нулевое
        {
            Quaternion lookY = Quaternion.LookRotation(dirXZ.normalized, Vector3.up); // Поворачиваем к цели
            float localX = -AngleInDegrees; // Угол по оси X
            Vector3 newEuler = new Vector3(localX, lookY.eulerAngles.y, 0f); // Новый угол поворота
            SpawnTransform.rotation = Quaternion.Euler(newEuler); // Применяем новый угол
        }

        // Проверяем, пора ли стрелять
        if (shotTimer >= Timer)
        {
            shotTimer = 0f; // Сбрасываем таймер
            AimToTarget();
            Shoot(); // Выполняем выстрел
        }
    }

    public void AimToTarget()
    {
        Vector3 dir = (TargetTransform.position + Vector3.up * (5f+ Random.Range(-2,2))) - bow.position ; // Вычисляем направление к цели

        
        bow.rotation = Quaternion.LookRotation(dir); // Применяем вращение
    }


    public void Shoot()
    {
        // Создаем новую пулю
        GameObject newBullet = Instantiate(BulletPrefab, SpawnTransform.position, SpawnTransform.rotation);
        newBullet.layer = LayerMask.NameToLayer("BulletEnemy");
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>(); // Получаем Rigidbody пули
        bulletRb.velocity = bow.forward *  (Vector3.Distance(TargetTransform.position,bow.position) +10); // Устанавливаем скорость пули 
 
    }
}