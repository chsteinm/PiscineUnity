using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 2f;
    private Transform target;
    private int currentPathIndex = 0;

    public float health = 3f;

    private void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        target = LevelManager.main.path[currentPathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.05f)
        {
            currentPathIndex++;
            if (currentPathIndex < LevelManager.main.path.Length)
            {
                target = LevelManager.main.path[currentPathIndex];
            }
            else
            {
                LevelManager.main.TakeDamage();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

}
