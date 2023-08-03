using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{    
    ObjectPool<Bullet> BulletPool;
    public Bullet bullet;
    public float shotDelay, shotDelayMax;

    private void Start()
    {
        BulletPool = new ObjectPool<Bullet>(bullet.gameObject);
    }

    private void Update()
    {
        shotDelay += Time.deltaTime;

        if(shotDelay >= shotDelayMax)
        {
            BulletPool.GetObj().init();
            shotDelay = 0;
        }
    }
}
