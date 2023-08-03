using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Unit
{
    public static Player instance;

    public Rigidbody rigid;
    public bool isDrag, isForce, isNoHit;
    public LayerMask playerLayer;
    public Vector3 dragStartPos;
    Camera cam;
    public Transform firePos;
    public GameObject ForceObj;

    private void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<Camera>();
    }

    public void Start()
    {
        LevelSet();
    }

    private void Update()
    {
        Move();
        DragAndMove();
        UseForce();
    }

    void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(hAxis, 0, vAxis) * Speed * Time.deltaTime;
    }

    void DragAndMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool isHit = Physics.Raycast(ray, out hit, 120f, playerLayer);

            if (isHit)
            {
                dragStartPos = hit.point;
                isDrag = true;
            }
        }

        if(Input.GetMouseButtonUp(0) && isDrag)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool isHit = Physics.Raycast(ray, out hit, 120f);

            if (isHit)
            {
                Vector3 dir = dragStartPos - hit.point;
                dir.y = 0;
                rigid.AddForce(dir * (Speed / 2), ForceMode.Impulse);
            }
        }
    }

    void UseForce()
    {
        ForceObj.transform.position = transform.position;

        if (Input.GetMouseButtonDown(1))
            if(!isForce)
                StartCoroutine(Force());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BossForce")
        {
            Vector3 reactVec = transform.position - other.gameObject.transform.position;
            rigid.AddForce(reactVec * 3.75f, ForceMode.Impulse);
        }

        if (other.tag == "Walls" && !isNoHit)
        {
            gameObject.SetActive(false);
            Debug.Log("Game Over");
            ScenesManager.instance.GameEnd();
        }    
    }

    public void LevelSet()
    {
        HP += PlayerPrefs.GetInt("HP_Level");
        Speed += PlayerPrefs.GetInt("Speed_Level");
    }

    public IEnumerator OnReact(Vector3 react)
    {
        rigid.AddForce(react * 3, ForceMode.Impulse);
        yield return new WaitForSeconds(.5f);
    }

    public IEnumerator Force()
    {
        isNoHit = true;
        isForce = true;
        ForceObj.SetActive(true);
        yield return new WaitForSeconds(5f);
        ForceObj.SetActive(false);
        isForce = false;
        isNoHit = false;
    }
}
