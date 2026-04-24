using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;

    public int rows = 5;
    public int cols = 10;

    public float xSpacing = 2.5f;
    public float ySpacing = 2.0f;
    public float enemyInsetDefault = 1.5f;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        float startX = -(cols - 1) * xSpacing * 0.5f;
        float startY = Camera.main.orthographicSize - enemyInsetDefault - 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 pos = new Vector3(
                    startX + col * xSpacing,
                    startY - row * ySpacing,
                    0
                );

                Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
