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

    public float gravity = -9.81f; 
    
    
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
        if (SpawnTransform == null || TargetTransform == null || bow == null) return;

        Vector3 spawnPos = SpawnTransform.position;
        Vector3 targetPos = TargetTransform.position + Vector3.up * (5f + Random.Range(-2, 2)); // как у вас было
        if (GetLaunchVelocity(spawnPos, targetPos, out Vector3 launchVel))
        {
            if (launchVel.sqrMagnitude > 0.0001f)
            {
                bow.rotation = Quaternion.LookRotation(launchVel.normalized, Vector3.up);
            }
        }
    }
    
    
    private bool GetLaunchVelocity(Vector3 spawnPos, Vector3 targetPos, out Vector3 launchVelocity)
    {
        launchVelocity = Vector3.zero;
        float g = Mathf.Abs(Physics.gravity.y);

        Vector3 flatTarget = new Vector3(targetPos.x, spawnPos.y, targetPos.z);
        float distance = Vector3.Distance(spawnPos, flatTarget);
        float heightDiff = targetPos.y - spawnPos.y;
        if (distance <= 0.0001f) return false;

        float bestV0 = float.PositiveInfinity;
        float bestAngle = float.NaN;
        // Поиск в диапазоне 30..45° минимального v0
        for (float deg = 30f; deg <= 45f; deg += 0.5f)
        {
            float a = deg * Mathf.Deg2Rad;
            float cosA = Mathf.Cos(a);
            float tanA = Mathf.Tan(a);
            float denom = 2f * cosA * cosA * (distance * tanA - heightDiff);
            if (denom <= 0f) continue;
            float v0sqr = g * distance * distance / denom;
            if (v0sqr <= 0f || float.IsNaN(v0sqr)) continue;
            float v0 = Mathf.Sqrt(v0sqr);
            if (v0 < bestV0) { bestV0 = v0; bestAngle = a; }
        }

        if (float.IsInfinity(bestV0))
        {
            for (float deg = 5f; deg <= 85f; deg += 1f)
            {
                float a = deg * Mathf.Deg2Rad;
                float cosA = Mathf.Cos(a);
                float tanA = Mathf.Tan(a);
                float denom = 2f * cosA * cosA * (distance * tanA - heightDiff);
                if (denom <= 0f) continue;
                float v0sqr = g * distance * distance / denom;
                if (v0sqr <= 0f || float.IsNaN(v0sqr)) continue;
                float v0 = Mathf.Sqrt(v0sqr);
                if (v0 < bestV0) { bestV0 = v0; bestAngle = a; }
            }
        }

        if (float.IsInfinity(bestV0) || float.IsNaN(bestAngle)) return false;

        Vector3 dirXZ = (flatTarget - spawnPos).normalized;
        float cosBest = Mathf.Cos(bestAngle);
        float sinBest = Mathf.Sin(bestAngle);
        launchVelocity = dirXZ * (bestV0 * cosBest) + Vector3.up * (bestV0 * sinBest);
        return true;
    }

    public void Shoot()
    {
        // Instantiate bullet
        Vector3 spawnPos = SpawnTransform.position;
        Vector3 targetPos = TargetTransform.position; // используем реальную позицию цели (центр)
    
        // небольшое смещение вперёд, чтобы не пересекать коллайдер спавна
        Vector3 dirFlatTemp = new Vector3(targetPos.x - spawnPos.x, 0f, targetPos.z - spawnPos.z).normalized;
        spawnPos += dirFlatTemp * 0.5f;

        GameObject newBullet = Instantiate(BulletPrefab, spawnPos, SpawnTransform.rotation);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        rb.gameObject.layer = LayerMask.NameToLayer("BulletEnemy");
        if (rb == null) { Debug.LogError("Bullet prefab needs Rigidbody."); Destroy(newBullet); return; }

        // Попытка получить начальную скорость через общий метод
        if (!GetLaunchVelocity(spawnPos, targetPos, out Vector3 launchVel))
        {
            Debug.LogError("No valid angle/speed found for hitting target.");
            Destroy(newBullet);
            return;
        }

        // Установим вращение лука в направлении начальной скорости (если нужен именно здесь)
        if (bow != null && launchVel.sqrMagnitude > 0.0001f)
        {
            bow.rotation = Quaternion.LookRotation(launchVel.normalized, Vector3.up);
        }

        // Применяем скорость в мировых координатах
        rb.useGravity = true;
        rb.velocity = launchVel;
    }
    
    
    
}