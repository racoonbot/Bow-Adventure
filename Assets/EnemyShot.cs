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

    public float Gravity = -9.81f; 
    
    
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
        GameObject newBullet = Instantiate(BulletPrefab, SpawnTransform.position, SpawnTransform.rotation);
        newBullet.layer = LayerMask.NameToLayer("BulletEnemy");

        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>(); // Получаем Rigidbody пули

        // Учитываем координаты цели, добавляя небольшую высоту
        Vector3 targetPosition = TargetTransform.position + Vector3.up * (5f + Random.Range(-2, 2));

        // Рассчитываем направление к цели (от спавна к цели)
        Vector3 direction = (targetPosition - SpawnTransform.position).normalized; // Направление к цели

        // Угловая скорость
        float angle = 45f * Mathf.Deg2Rad;

        // Дистанция до цели
        float distance = Vector2.Distance(new Vector2(SpawnTransform.position.x, SpawnTransform.position.y), new Vector2(targetPosition.x, targetPosition.y));

        // Рассчитываем начальную скорость
        float initialVelocity = Mathf.Sqrt(distance * Mathf.Abs(Gravity) / Mathf.Sin(2 * angle));

        // Создаем вектор скорости, направленный по линии к цели
        Vector2 launchVelocity = direction * initialVelocity;

        // Устанавливаем скорость пули
        bulletRb.velocity = launchVelocity; // Устанавливаем скорость пули
    }


}