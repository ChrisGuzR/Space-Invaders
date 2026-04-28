using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }
    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Dynamic")] [Range(0, 4)]
    private float _shieldLevel = 1;
    //[SerializeField] private float speed = 30f;

    [Tooltip("This field holds a reference to the last triggering GameObject")]
    private GameObject lastTriggerGo = null;

    public delegate void WeaponFireDelegate();
    public event WeaponFireDelegate fireEvent;


    // Start is called before the first frame update
    void Awake()
    {
        if (S == null)
        {
            S = this; // Set the singleton
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }

        ClearWeapons();
        weapons[0].SetType(eWeaponType.blaster);
    }


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += h * speed * Time.deltaTime;
        //pos.y += v * speed * Time.deltaTime;
        transform.position = pos;

        //transform.rotation = Quaternion.Euler(v * pitchMult, h * rollMult, 0);
        transform.rotation = Quaternion.Euler(0, h * rollMult, 0);

        if (Input.GetAxis("Jump") == 1 && fireEvent != null)
        {
            fireEvent();
        }
    }

    

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo) return;
        lastTriggerGo = go;

        EnemyBullet bullet = go.GetComponent<EnemyBullet>();
        if ( bullet != null)
        {
            shieldLevel--;
            Destroy(go);
            return;
        }
        Enemy enemy = go.GetComponent<Enemy>();
        PowerUp pUp = go.GetComponent<PowerUp>();
        EnemyManager man = go.GetComponent<EnemyManager>();
        if (enemy != null || man != null)
        {
            shieldLevel--;
            Destroy(go);
            Main.HERO_DIED();


        }
        else if(pUp != null)
        {
            AbsorbPowerUp(pUp);
        }

        else
        {
            Debug.LogWarning("Shield trigger hit by non-Enemy: " + go.name);
        }
    }

    public void AbsorbPowerUp(PowerUp pUp)
    {
        Debug.Log("Absorbing powerup: " + pUp.type);
        switch (pUp.Type)
        {
            case eWeaponType.shield:
                shieldLevel++;
                break;
            default:
                if (pUp.Type == weapons[0].type)
                {
                    Weapon weap = GetEmptyWeaponSlot();
                    if (weap != null)
                    {
                        weap.SetType(pUp.Type);
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].SetType(pUp.Type);
                }
                break;
        }
        pUp.AbsorbedBy(this.gameObject);
    }

    public float shieldLevel
    {
        get { return _shieldLevel; }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(this.gameObject);
                Main.HERO_DIED();
            }
        }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == eWeaponType.none)
            {
                return weapons[i];
            }
        }
        return null;
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(eWeaponType.none);
        }
    }

    
}
