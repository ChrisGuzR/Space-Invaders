using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotSpeed = 0.1f;
    [Header("Dynamic")]
    public int level = 0;


    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        int currlevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if (level != currlevel)
        {
            level = currlevel;
            mat.mainTextureOffset = new Vector2(0.2f * level, 0);
        }
        float rZ = -(rotSpeed * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);

        
    }
}
