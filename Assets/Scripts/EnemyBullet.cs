using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    protected BoundCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundCheck>();

    }
    void Update()
    {
        if (bndCheck.LocIs(BoundCheck.eScreenLocs.offDown))
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


}
