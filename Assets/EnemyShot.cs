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
        Vector3 dir = TargetTransform.position - SpawnTransform.position; // Вычисляем направление к цели
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Вычисляем угол в градусах
        angle -= AngleInDegrees;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle); // Создаем вращение вокруг оси Z
        transform.rotation = rotation; // Применяем вращение
    }


    public void Shoot()
    {
        // Создаем новую пулю
        GameObject newBullet = Instantiate(BulletPrefab, SpawnTransform.position, SpawnTransform.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>(); // Получаем Rigidbody пули
        Vector3 from = SpawnTransform.position; // Позиция спавна
        Vector3 to = TargetTransform.position; // Позиция цели
        Vector3 diff = to - from; // Разница между позициями (direction)
        float x = new Vector3(diff.x, 0f, diff.z).magnitude; // Горизонтальное расстояние до цели
        float y = diff.y; // Вертикальное расстояние до цели
        float angleRad = AngleInDegrees * Mathf.Deg2Rad; // Преобразуем угол в радианы
        float g = Physics.gravity.y; // Получаем значение гравитации

        // Вычисляем необходимые параметры для стрельбы
        float cosA = Mathf.Cos(angleRad);
        float sinA = Mathf.Sin(angleRad);
        float tanA = Mathf.Tan(angleRad);

        float denom = 2f * cosA * cosA * (y - x * tanA); // Знаменатель для вычисления скорости

        float v2 = (g * x * x) / denom; // Вычисляем квадрат скорости
        float v = Mathf.Sqrt(v2); // Получаем скорость
        shootDir = SpawnTransform.forward; // Направление стрельбы
        float speedRandomPercient = 0.30f; // 30% разброс  к скорости
        float speedFactor = 1f + Random.Range(-speedRandomPercient, speedRandomPercient);
        bulletRb.velocity = shootDir.normalized * v * speedFactor; // Устанавливаем скорость пули 
        // bulletRb.velocity = shootDir.normalized * v; 
    }
}