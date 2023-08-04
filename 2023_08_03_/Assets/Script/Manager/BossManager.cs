using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    public Boss BossPrefabs; 
    public Boss Boss;

    public float bossSpawnTime , bossSpawnTimeMax;
    public Image bossHp;

    public GameObject SpawnManagerObj;

    public float BossHealth, BossMaxHealth;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        CheckBossTime();
        BossHealthBarSet();
    }

    public void CheckBossTime()
    {
        bossSpawnTime += Time.deltaTime;

        if(bossSpawnTime >= bossSpawnTimeMax)
        {
            Boss = Instantiate(BossPrefabs, transform.position, transform.rotation);
            bossSpawnTime = 0;
        }
        
    }

    public void BossHealthBarSet()
    {
        if(Boss.gameObject.activeInHierarchy)
        {
            bossSpawnTime = 0;
            bossHp.gameObject.SetActive(Boss.gameObject.activeSelf);
            bossHp.transform.GetComponentInChildren<Image>().fillAmount = BossMaxHealth / BossHealth;
        }

    }
}
