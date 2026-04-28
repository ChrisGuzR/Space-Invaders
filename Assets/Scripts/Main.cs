using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S; //Singleton
    static private Dictionary<eWeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Inscribed")]
    public bool SpawnEnemies = true;
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public float gameRestartDelay = 1f;
    public GameObject prefabPowerUp;
    public WeaponDefinition[] weaponDefinitions;
    public eWeaponType[] powerUpFrequency = new eWeaponType[] {
        eWeaponType.blaster, eWeaponType.blaster,
        eWeaponType.spread, eWeaponType.shield
    };

    public int cols = 10;
    public float spacing = 2f;

    private BoundCheck bndCheck;

     void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundCheck>();
        //Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    
    /*public void SpawnEnemy()
    {
        if (!SpawnEnemies)
        {
            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
            return;
        }

        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        float enemyInset = enemyInsetDefault;
        if (go.GetComponent<BoundCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundCheck>().radius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax = bndCheck.camWidth - enemyInset;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }*/

    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        SceneManager.LoadScene("DeathScene");
    }

    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }

    static public WeaponDefinition GetWeaponDefinition(eWeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return WEAP_DICT[wt];
        }
        return (new WeaponDefinition());
    }

    static public void SHIP_DESTROYED(Enemy e)
    {
        if (Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, S.powerUpFrequency.Length);
            eWeaponType pUpType = S.powerUpFrequency[ndx];

            GameObject go = Instantiate<GameObject>(S.prefabPowerUp);
            PowerUp pUp = go.GetComponent<PowerUp>();

            pUp.SetType(pUpType);
            
            pUp.transform.position = e.transform.position;
        }
    }
}
