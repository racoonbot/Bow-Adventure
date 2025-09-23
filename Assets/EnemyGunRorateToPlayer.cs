using UnityEngine;

public class EnemyGunRotateToPlayer : MonoBehaviour
{
    public Transform SpawnTransform;
    public Transform TargetTransform;
    public GameObject BulletPrefab;
    [Tooltip("Elevation angle of the barrel in degrees (positive = up).")]
    public float AngleInDegrees = 30f;

    [Tooltip("Seconds between shots.")]
    public float Timer = 2f;

    private float shotTimer;

    void Start()
    {
        if (SpawnTransform != null)
        {
            // Устанавливаем локальный наклон по X (баррель направлен вверх на AngleInDegrees)
            Vector3 e = SpawnTransform.localEulerAngles;
            e.x = -AngleInDegrees; // в вашем коде был минус — сохраняю логику (баррель смотрит вверх)
            SpawnTransform.localEulerAngles = e;
        }
    }

    void Update()
    {
        if (SpawnTransform == null || TargetTransform == null) return;

        shotTimer += Time.deltaTime;

        // Поворачиваем поворот вокруг Y так, чтобы баррель смотрел в сторону цели по горизонтали
        Vector3 dir = TargetTransform.position - SpawnTransform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        if (dirXZ.sqrMagnitude > 0.0001f)
        {
            Quaternion lookY = Quaternion.LookRotation(dirXZ.normalized, Vector3.up);
            // Сохраняем локальный X как угол возвышения (в локальных градусах)
            float localX = -AngleInDegrees;
            Vector3 newEuler = new Vector3(localX, lookY.eulerAngles.y, 0f);
            SpawnTransform.rotation = Quaternion.Euler(newEuler);
        }

        if (shotTimer >= Timer)
        {
            shotTimer = 0f;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (BulletPrefab == null)
        {
            Debug.LogError("BulletPrefab is not assigned!");
            return;
        }

        if (SpawnTransform == null || TargetTransform == null)
        {
            Debug.LogError("SpawnTransform or TargetTransform is not assigned!");
            return;
        }

        // Instantiate bullet and get its Rigidbody
        GameObject newBullet = Instantiate(BulletPrefab, SpawnTransform.position, SpawnTransform.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        if (bulletRb == null)
        {
            Debug.LogError("Instantiated bullet has no Rigidbody!");
            Destroy(newBullet);
            return;
        }

        // World positions
        Vector3 from = SpawnTransform.position;
        Vector3 to = TargetTransform.position;

        // Horizontal distance (x) and vertical difference (y)
        Vector3 diff = to - from;
        float x = new Vector3(diff.x, 0f, diff.z).magnitude;
        float y = diff.y;

        // Convert angle to radians; angle is elevation above horizontal
        float angleRad = AngleInDegrees * Mathf.Deg2Rad;

        // Use Physics.gravity.y (negative in Unity by default)
        float g = Physics.gravity.y;
        if (Mathf.Approximately(g, 0f))
        {
            Debug.LogError("Physics.gravity.y is zero — cannot compute ballistic velocity.");
            return;
        }

        // Formula for v^2 derived from projectile motion with given angle:
        // v^2 = (g*x^2) / (2 * (cos^2(angle) * (y - x*tan(angle))))
        float cosA = Mathf.Cos(angleRad);
        float sinA = Mathf.Sin(angleRad);
        float tanA = Mathf.Tan(angleRad);

        float denom = 2f * cosA * cosA * (y - x * tanA);

        // Note: since g is negative, numerator (g * x^2) is negative. denom must also be negative to get positive v^2.
        float v2 = (g * x * x) / denom;

        if (float.IsNaN(v2) || v2 <= 0f)
        {
            Debug.LogWarning("No valid ballistic solution for the given angle and target. Try a different angle or position.");
            // Optional fallback: aim directly toward target with a fixed speed
            // float fallbackSpeed = 20f;
            // bulletRb.velocity = (diff.normalized) * fallbackSpeed;
            return;
        }

        float v = Mathf.Sqrt(v2);

        // Build shoot direction in world space: start with forward vector in SpawnTransform local space
        // SpawnTransform rotation already set so that its forward points toward target horizontally and X tilt is elevation.
        Vector3 shootDir = SpawnTransform.forward; // uses current rotation (including elevation)

        // Set the rigidbody's velocity
        bulletRb.velocity = shootDir.normalized * v;
    }
}
