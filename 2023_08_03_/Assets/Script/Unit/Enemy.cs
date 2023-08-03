using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public enum Rank { Normal, Rare, Epic, Boss }
    public Rank rank;

    public enum Value { One, Two }
    public Value value;

    public int score, dropGold;
    public Transform firePos;
    public Vector3 startPos;
    public Rigidbody rigid;

    public bool isLook, isAttack, isDead, isForceAttack;

    public float attackDelay, attackDelayMax;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>(); 
    }

    private void Start()
    {
        startPos = transform.position;

        switch (rank)
        {
            case Rank.Normal:
                HP = 4;
                Speed = 5;
                score = 10;
                dropGold = 10;
                break;
            case Rank.Rare:
                HP = 4;
                Speed = 7;
                score = 30;
                dropGold = 20;
                break;
            case Rank.Epic:
                HP = 7;
                Speed = 6;
                score = 35;
                dropGold = 40;
                break;
            case Rank.Boss:
                HP = 150;
                Speed = 10;
                score = 10;
                dropGold = 500;
                break;
        }
    }

    private void Update()
    {
        if(rank != Rank.Boss)
        {
            lookAt();
            Attack();
        }
        
        DeadEvent();
        Move();
        inReturn();
    }

    void Move()
    {
        if (!isAttack)
            rigid.AddForce(transform.forward * Speed * Time.deltaTime, ForceMode.Impulse);
    }

    void DeadEvent()
    {
        if (HP <= 0 && !isDead)
        {
            isDead = true;
            DataManager.instance.Score += (score * 2);
            int addGold = PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", addGold += dropGold);
            gameObject.SetActive(false);
        }
    }

    void Attack()
    {
        if(gameObject.activeInHierarchy)
        {
            if(rank != Rank.Boss)
            {
                attackDelay += Time.deltaTime;

                if (attackDelay >= attackDelayMax && !isAttack)
                {
                    StartCoroutine(AttackCo());
                }
            }
        }
    }

    void lookAt()
    {
        if (isLook && rank != Rank.Boss)
            transform.LookAt(Player.instance.transform.position);
    }

    void inReturn()
    {
        if(transform.position.z <= -15)
        {
            transform.position = startPos;
            //gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if(bullet.target == Target.Enemy)
            {
                if(rank != Rank.Boss)
                {
                    Vector3 reactVec = transform.position - other.gameObject.transform.position;
                    StartCoroutine(OnReact(reactVec));
                }
            }
        }

        if (other.tag == "Force")
        {
            isForceAttack = true;
            HP -= 1;
            Vector3 reactVec = transform.position - other.gameObject.transform.position;
            StartCoroutine(OnReact(reactVec));
        }

        if (other.tag == "Walls")
        {
            if (isForceAttack)
                HP -= 1;

            Vector3 reactVec = transform.position - other.gameObject.transform.position;
            StartCoroutine(OnReact(reactVec));
        }
    }

    public IEnumerator OnReact(Vector3 react)
    {
        DataManager.instance.Score += score;
        rigid.AddForce(react * 3, ForceMode.Impulse);
        yield return new WaitForSeconds(.5f);
        isForceAttack = false;
    }

    public IEnumerator AttackCo()
    {
        switch(rank)
        {
            case Rank.Rare:
                switch(value)
                {
                    case Value.One:
                        isLook = true;
                        isAttack = true;
                        yield return new WaitForSeconds(.5f);
                        isLook = false;
                        yield return new WaitForSeconds(.25f);
                        rigid.AddForce(transform.forward * Speed, ForceMode.Impulse);
                        yield return new WaitForSeconds(1.5f);
                        rigid.velocity = Vector3.zero;
                        yield return new WaitForSeconds(.25f);
                        attackDelay = 0;
                        isAttack = false;
                        break;
                    case Value.Two:
                        isLook = true;
                        isAttack = true;
                        yield return new WaitForSeconds(.5f);
                        isLook = false;
                        yield return new WaitForSeconds(.25f);
                        rigid.AddForce(transform.forward * Speed, ForceMode.Impulse);
                        yield return new WaitForSeconds(.5f);
                        rigid.AddForce(transform.right * Speed, ForceMode.Impulse);
                        yield return new WaitForSeconds(.5f);
                        rigid.AddForce(-transform.right * Speed, ForceMode.Impulse);
                        yield return new WaitForSeconds(.5f);
                        rigid.AddForce(-transform.forward * Speed, ForceMode.Impulse);
                        yield return new WaitForSeconds(.5f);
                        rigid.velocity = Vector3.zero;
                        attackDelay = 0;
                        isAttack = false;
                        break;
                }
                break;
        }
    }
}
