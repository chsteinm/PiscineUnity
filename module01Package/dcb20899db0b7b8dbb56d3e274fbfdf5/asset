using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject RotateCenter;
    public Transform firePoint; // optional: where projectiles are firePointed
    public GameObject projectilePrefab;

    public float damage = 1f;
    public float range = 5f;
    public float fireRate = 1f;
    public float projectileSpeed = 8f;
    public float rotateSpeed = 720f;

    private float fireCooldown = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (RotateCenter == null) return;

        GameObject target = GetClosestEnemy();
        if (target == null) return;

        Vector3 direction = target.transform.position - RotateCenter.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Smoothly rotate the RotateCenter to face the target
        RotateCenter.transform.rotation = Quaternion.RotateTowards(RotateCenter.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Firing logic: only if within range
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (dist <= range)
        {
            if (fireCooldown <= 0f)
            {
                Fire();
                fireCooldown = 1f / Mathf.Max(0.0001f, fireRate);
            }
        }

        if (fireCooldown > 0f) fireCooldown -= Time.deltaTime;
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = float.MaxValue;
        Vector3 origin = RotateCenter != null ? RotateCenter.transform.position : transform.position;

        foreach (GameObject e in enemies)
        {
            if (e == null) continue;
            float d = Vector3.Distance(origin, e.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = e;
            }
        }

        if (closest == null) return null;
        return (minDist <= range) ? closest : null;
    }

    private void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("TurretController: projectilePrefab not set.");
            return;
        }

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, RotateCenter.transform.rotation);
        ProjectileController projectileController = proj.GetComponent<ProjectileController>();
        if (projectileController != null)
        {
            projectileController.SetDamage(damage);
        }

        Rigidbody2D rb2d = proj.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.linearVelocity = RotateCenter.transform.up * projectileSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = (RotateCenter != null) ? RotateCenter.transform.position : transform.position;
        Gizmos.DrawWireSphere(origin, range);
    }
}
