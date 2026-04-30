using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
     public float speed = 2f;
    public float dropDistance = 0.5f;

    private float direction = 1; // 1 = right, -1 = left

    public GameObject bulletPrefab;
    public float shootChance = 0.01f;
    public int level = 1;
    bool spawningNext = false;

    void Update()
    {
        TryShoot();
        
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (ReachedEdge())
        {
            Vector3 downDistantce = new Vector3(0,0,dropDistance);
            direction *= -1;
            transform.position += Vector3.down + downDistantce;
        }
        if (GetComponentsInChildren<Enemy>().Length == 0 && !spawningNext)
        {
        spawningNext = true;
        Invoke(nameof(NextLevel), 2f);
        }
        }

    void NextLevel()
    {
        level++;
        
        Debug.Log("Level: " + level);
        IncreaseDifficulty();
        SpawnNewWave();
    }

    void SpawnNewWave()
    {
        EnemySpawner spawner = GetComponent<EnemySpawner>();
        if(level == 1)
        {
            spawner.SpawnGrid();
            spawningNext = false;
        }
        
    }

    void IncreaseDifficulty()
    {
        speed += 0.5f;
        shootChance += 0.002f;
    }

    void TryShoot()
    {
        if (Random.value > shootChance * Time.deltaTime) return;

        Dictionary<int, Transform> bottomEnemies = new Dictionary<int, Transform>();

        foreach (Transform enemy in transform)
        {
                // group enemies by column (approximate using x position)
            int colKey = Mathf.RoundToInt(enemy.position.x * 10);

            if (!bottomEnemies.ContainsKey(colKey))
            {
                bottomEnemies[colKey] = enemy;
            }
            else
            {
                // pick the LOWER enemy (smaller y)
                if (enemy.position.y < bottomEnemies[colKey].position.y)
                {
                    bottomEnemies[colKey] = enemy;
                }
            }
        }

        List<Transform> shooters = new List<Transform>(bottomEnemies.Values);

        if (shooters.Count == 0) return;

        Transform shooter = shooters[Random.Range(0, shooters.Count)];

        GameObject bullet = Instantiate(bulletPrefab, shooter.position, Quaternion.identity);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * 10f;
    }

    bool ReachedEdge()
    {
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        foreach (Transform enemy in transform)
        {
            BoundCheck bc = enemy.GetComponent<BoundCheck>();
            if (bc == null) continue;

            float halfWidth = bc.radius;

            float x = enemy.position.x;

            if (x + halfWidth >= screenHalfWidth)
                return true;

            if (x - halfWidth <= -screenHalfWidth)
                return true;
        }

        return false;
    }
}
