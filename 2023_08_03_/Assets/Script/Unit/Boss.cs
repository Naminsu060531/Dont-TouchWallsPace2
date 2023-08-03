using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : Enemy
{
    public bool isThink, isRight, isLooks;
    public GameObject ForceObj;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Think());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        lookAts();
    }

    void Move()
    {
        if(!isThink)
        {
            if (transform.position.x <= -15)
                isRight = true;

            if (transform.position.x >= 15)
                isRight = false;

            float dir = isRight ? 1f : -1f;

            Vector3 nextPos = transform.position + new Vector3(dir * (Speed / 2) * Time.deltaTime, 0, 0);

            transform.position = nextPos;
        }
    }

    void lookAts()
    {
        if (!isLooks)
            transform.LookAt(Player.instance.transform.position);
    }

    IEnumerator Think()
    {
        isThink = true;
        yield return null;

        int ranValue = Random.Range(0, 2);

        switch(ranValue)
        {
            case 0:
                StartCoroutine(Dash());
                break;
            case 1:
                StartCoroutine(Force());
                break;
        }

    }

    IEnumerator Dash()
    {
        Debug.Log("Dash");
        isLooks = true;
        yield return new WaitForSeconds(1f);
        rigid.AddForce(transform.forward * Speed, ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
        isLooks = false;
        isThink = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Think());
    }

    IEnumerator Force()
    {
        Debug.Log("Force");

        if(!ForceObj.activeInHierarchy)
            ForceObj.SetActive(true);
        StartCoroutine(Think());
        yield return new WaitForSeconds(5f);
        ForceObj.SetActive(false);
    }
}
