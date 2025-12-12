using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        // Destroy the projectile after one sec
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void SetDamage(float value)
    {
        damage = value;
    }
}
