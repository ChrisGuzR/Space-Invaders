using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
     public float speed = 2f;
    public float dropDistance = 0.5f;

    private int direction = 1; // 1 = right, -1 = left

    public GameObject bulletPrefab;
    public float shootChance = 0.01f;

    void Update()
    {
        TryShoot();
        
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

    if (ReachedEdge())
    {
        direction *= -1;
        transform.position += Vector3.down * dropDistance;
    }
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
