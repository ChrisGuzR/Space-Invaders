using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


}
