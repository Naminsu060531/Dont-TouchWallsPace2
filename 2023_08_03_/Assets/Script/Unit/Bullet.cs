using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Unit
{
    Player player;
    public bool isPlayer, isEnemy;
    public int dmg;

    private void Awake()
    {
         player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        if (target == Target.Player)
        {
            isPlayer = true;
        }
        else if(target == Target.Enemy)
        {
            isEnemy = true;
            dmg += PlayerPrefs.GetInt("Attack_Level");
        }
    }

    private void Update()
    {
        if (isPlayer)
            transform.position += -transform.forward * Speed * Time.deltaTime;
        else if (isEnemy)
            transform.position += transform.forward * Speed * Time.deltaTime;
    }

    public void init()
    {
        transform.position = player.firePos.position;

        if(!Player.instance.isForce)
        {
            gameObject.SetActive(true);
            Invoke("UnSet", 8f);
        }
    }

    void UnSet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer)
        {
            if (other.tag == "Player")
            {
                Player player = other.GetComponent<Player>();
                player.HP -= dmg;
                gameObject.SetActive(false);
            }
        }

        if (isEnemy)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("*");
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.HP -= dmg;
                gameObject.SetActive(false);
            }
        }
    }
}
