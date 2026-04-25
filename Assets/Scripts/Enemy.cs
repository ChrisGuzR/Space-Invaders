using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public float powerUpDropChance = 1f;

    protected bool calledShipDestroyed = false;

    protected BoundCheck bndCheck;
    void Awake()    {
        bndCheck = GetComponent<BoundCheck>();
    }
    // Start is called before the first frame update
    public Vector3 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.LocIs(BoundCheck.eScreenLocs.offDown))
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        
    }



    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        EnemyBullet eb = otherGO.GetComponent<EnemyBullet>();
        if (p != null)
        {
            if (bndCheck.isOnScreen)
            {
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    if (!calledShipDestroyed)
                    {
                        calledShipDestroyed = true;
                        Main.SHIP_DESTROYED(this);
                    }
                    Destroy(this.gameObject);
                }
            }
                Destroy(otherGO);
        }else if (eb != null)
        {
            // Enemy should NOT react to its own bullets
            return;
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }


}
