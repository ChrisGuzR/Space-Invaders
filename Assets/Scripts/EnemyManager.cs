using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
     public float speed = 2f;
    public float dropDistance = 0.5f;

    private int direction = 1; // 1 = right, -1 = left
    bool hasReachedEdge = false;

    void Update()
    {
        
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

    if (ReachedEdge())
    {
        direction *= -1;
        transform.position += Vector3.down * dropDistance;
    }
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
