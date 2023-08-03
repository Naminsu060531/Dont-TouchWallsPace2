using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public Boss BossPrefabs; 
    public Boss Boss;

    public float bossSpawnTime;
    public Image bossHp;

    public GameObject SpawnManagerObj;

    public float BossHealth, BossMaxHealth;

    private void Start()
    {
        Boss = Instantiate(BossPrefabs, transform.position, transform.rotation);
        bossHp.gameObject.SetActive(Boss.gameObject.activeSelf);
        BossMaxHealth = BossPrefabs.HP;
    }

    private void Update()
    {
        SpawnManagerObj.SetActive(Boss.gameObject.activeSelf);
        BossHealth = BossPrefabs.HP;
        bossHp.transform.GetComponentInChildren<Image>().fillAmount = BossHealth / BossMaxHealth;
    }
}
