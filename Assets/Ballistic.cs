using UnityEngine;

public class Ballistic : MonoBehaviour
{
    public Transform SpawnTransform;
    public Transform TargetTransform;
    private float g = Physics.gravity.y;
    public GameObject BulletPrefab;
    public float AngleInDegrees;

    private void Start()
    {
        SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0, 0);
    }

    void Update()
    {
        if (SpawnTransform == null || TargetTransform == null)
        {
            Debug.LogError("SpawnTransform or TargetTransform is not assigned!");
            return;
        }

        // Поворачиваем объект к цели
        Vector3 directionToTarget = TargetTransform.position - SpawnTransform.position;
        directionToTarget.y = 0; // Игнорируем высоту для поворота
        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            SpawnTransform.rotation = Quaternion.Euler(-AngleInDegrees, targetRotation.eulerAngles.y, 0);
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse button pressed, attempting to shoot...");
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

        // Проверка на наличие Rigidbody в BulletPrefab
        Rigidbody bulletRigidbody = BulletPrefab.GetComponent<Rigidbody>();
        if (bulletRigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing on BulletPrefab!");
            return;
        }

        Vector3 fromTo = TargetTransform.position - SpawnTransform.position;
        Vector3 fromToXz = new Vector3(fromTo.x, 0f, fromTo.z);

        // Устанавливаем угол для расчета направления пули
        float x = fromToXz.magnitude;
        float y = fromTo.y;

        Debug.Log($"Distance to target (x): {x}");
        Debug.Log($"Height difference to target (y): {y}");

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180f;
        Debug.Log($"Angle in radians: {AngleInRadians}");

        float v2 = (g * x * x) / (2 * (y - (Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2)));
        Debug.Log($"Calculated v2: {v2}");

        if (v2 < 0)
        {
            Debug.LogError("Calculated v2 is negative, cannot calculate velocity.");
            return;
        }

        float v = Mathf.Sqrt(Mathf.Abs(v2));
        Debug.Log($"Calculated initial velocity (v): {v}");

        GameObject NewBullet = Instantiate(BulletPrefab, SpawnTransform.position, Quaternion.identity);
        GameObject NewBullet2 = Instantiate(BulletPrefab, SpawnTransform.position, Quaternion.identity);
        GameObject NewBullet3 = Instantiate(BulletPrefab, SpawnTransform.position, Quaternion.identity);
        bulletRigidbody = NewBullet.GetComponent<Rigidbody>();
        var bulletRigidbody2 = NewBullet2.GetComponent<Rigidbody>();
        var bulletRigidbody3 = NewBullet3.GetComponent<Rigidbody>();

        if (bulletRigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing on the instantiated bullet!");
            return;
        }

        // Устанавливаем скорость пули в нужном направлении
        Vector3 shootDirection = Quaternion.Euler(-AngleInDegrees, 0, 0) * Vector3.forward;
        bulletRigidbody.velocity = shootDirection * v;
        
        Vector3 shootDirection2 = Quaternion.Euler(-AngleInDegrees-15, 0, 0) * Vector3.forward;
        bulletRigidbody2.velocity = shootDirection2 * v;
        
        Vector3 shootDirection3 = Quaternion.Euler(-AngleInDegrees+15, 0, 0) * Vector3.forward;
        bulletRigidbody3.velocity = shootDirection3 * v;
        Debug.Log($"Bullet instantiated and velocity set: {bulletRigidbody.velocity}");
    }
}
