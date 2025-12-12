using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public GameObject enemyPrefab;
    public Transform startPoint;
    public Transform[] path;

    public int baseHealth = 5;

    private void Awake()
    {
        main = this;
    }


    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 2f);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, startPoint.position, Quaternion.identity);
    }

    public void TakeDamage()
    {
        baseHealth -= 1;
        Debug.Log("Health: " + baseHealth);
        if (baseHealth <= 0)
        {
            // Handle level failure (e.g., restart level, show game over screen)
            Debug.Log("Game Over");
            // Stop spawning new enemies
            CancelInvoke("SpawnEnemy");
            // Destroy all existing enemies
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
}
