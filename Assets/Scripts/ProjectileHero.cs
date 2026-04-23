using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundCheck))]
public class ProjectileHero : MonoBehaviour
{
    private BoundCheck bndCheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField] 
    private eWeaponType _type;

    public eWeaponType type
    {
        get { return( _type); }
        set{ SetType(value); }
    }
    void Awake()    {
        bndCheck = GetComponent<BoundCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndCheck.LocIs(BoundCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
    }

    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }

    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }
}
