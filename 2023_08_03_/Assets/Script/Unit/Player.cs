using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Unit
{
    public static Player instance;

    public Rigidbody rigid;
    bool isDrag;
    public LayerMask playerLayer;
    public Vector3 dragStartPos;
    Camera cam;
    public Transform firePos;

    private void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {
        Move();
        DragAndMove();
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

            bool isHit = Physics.Raycast(ray, out hit, 300f, playerLayer);

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

            bool isHit = Physics.Raycast(ray, out hit, 300f);

            if (isHit)
            {
                Vector3 dir = dragStartPos - hit.point;
                dir.y = 0;
                rigid.AddForce(dir * (Speed / 2), ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BossForce")
        {
            Vector3 reactVec = transform.position - other.gameObject.transform.position;
            rigid.AddForce(reactVec * 3.75f, ForceMode.Impulse);
        }
    }

    public IEnumerator OnReact(Vector3 react)
    {
        rigid.AddForce(react * 3, ForceMode.Impulse);
        yield return new WaitForSeconds(.5f);
    }
}
