using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
   public GameObject enemyPrefab;
   public GameObject enemyPrefab2;
   public GameObject enemyPrefab3;
   public GameObject enemyPrefabBoss;



    public int rows = 3;
    public int cols = 5;

    public float xSpacing = 2.5f;
    public float ySpacing = 2.0f;
    public float enemyInsetDefault = 1.5f;
    public int level;
    public EnemyManager enemyManagerLevel;

    void Start()
    {
        enemyManagerLevel = GetComponent<EnemyManager>();
        SpawnGrid();

    }

    public void SpawnGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        float startX = -(cols - 1) * xSpacing * 0.5f;
        float startY = Camera.main.orthographicSize - enemyInsetDefault - 2f;
        level = (int)enemyManagerLevel.level;

        if (level == 1)
        {
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
        else if(level == 2)
        {
            for(int i = rows; i > 0; i--)
            {
                for(int j = 0; j < (cols - i * 2) / 2; j++)
                {

                }
                for (int j = 0; j < i; j++)
                {
                    Vector3 pos = new Vector3(
                        startX + j * xSpacing,
                        startY - i * ySpacing,
                        0
                    );


                }
            }
        }else if(level == 3)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Vector3 pos = new Vector3(
                        startX + col * xSpacing,
                        startY - row * ySpacing,
                        0
                    );
                    Instantiate(enemyPrefab2, pos, Quaternion.identity, transform);


                }
            }
        }
       
        else if (level == 4)
        {
           
                    Vector3 pos = new Vector3(
                        startX + 3 * xSpacing,
                        startY - 2.5f * ySpacing,
                        0
                    );
                    Instantiate(enemyPrefabBoss, pos, Quaternion.identity, transform);


               
        }
        else if (level == 5)
        {
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            print("Out of Level Bounds find me in enemySpawner");
        }
            
    }
}
