using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EnemyObj;
    public float spawnDelay, spawnDelayMax;
    bool isRight, isBoss;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Spawn();
    }

    void Move()
    {
        if (transform.position.x <= -15)
            isRight = true;

        if (transform.position.x >= 15)
            isRight = false;

        float dir = isRight ? 1f : -1f;

        Vector3 nextPos = transform.position + new Vector3(dir * 5 * Time.deltaTime, 0, 0);

        transform.position = nextPos;
    }

    void Spawn()
    {
        spawnDelay += Time.deltaTime;

        if(spawnDelay >= spawnDelayMax)
        {
            int ranValue = Random.Range(0, EnemyObj.Length);

            Instantiate(EnemyObj[ranValue], transform.position, transform.rotation);

            spawnDelay = 0;
        }
    }
}
