using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlGetAxis : MonoBehaviour
{

    private float horizontal = 0.0f;    // Player�� ������ ���� ��. (�¿�)
    private float vertical = 0.0f;      // Player�� ������ ���� ��. (����)
    private float h_rotate = 0.0f;      // player�� ȸ���� ��. (�¿� ȸ��)
    private float v_rotate = 0.0f;      // player�� ȸ���� ��. (���� ȸ��)


    private Transform tr;               // Player�� Transform 
    private float moveSpeed = 10.0f;    // Player�� �̵� �ӵ�
    private float rotSpeed = 120.0f;    // Player�� ȸ�� �ӵ�

    private Rigidbody rb;               // Player�� Rigidbody
    bool canMove;                       // Player�� �̵����� ����

    Vector3 center;                     // Player�� �߽ɺ�
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {

        // Transform ��ġ ����.
        tr = gameObject.GetComponent<Transform>();

        rb = gameObject.GetComponent<Rigidbody>();

        
    }

    private void FixedUpdate()
    {
        center = tr.position + tr.up * 1.0f;

        RaycastHit hit;


        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        h_rotate = Input.GetAxis("Mouse X");
        //v_rotate = Input.GetAxis("Mouse Y");



        // ���� �¿� �̵� ���� ���� ���
        moveDir = new Vector3(horizontal, 0, vertical);
        //Vector3 moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);


        var sphereCast = Physics.SphereCastAll(center, 0.35f, Vector3.up, 0f, 1 << 7);
        //Physics.SphereCast(center, 0.35f, Vector3.up, 0, 1<<7);

        if (moveDir == Vector3.zero)
        {
            canMove = false;
        }
        else if(moveDir != Vector3.zero && sphereCast == null) // movdDir != 000 , �����̷��� �ϰ� ���� ��, sphereCast == null, �浹ü�� ���� ���
        {
            // �̵��� �� �ְ� ���ش�.
            canMove = true;
        }



        if (canMove)
        {

            // Translate(�̵� ���� * �ӵ� * ���� �� * Time.deltaTime, ���� ��ǥ)
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        }

        // Vector3.up ���� �������� rotSpeed ��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h_rotate);


        Debug.Log(tr.transform.position);
        Debug.Log("Center: " + center);
        Debug.Log("moveDir: " + moveDir);
        Debug.Log("Cast: " + sphereCast);
        Debug.Log("canMove: " + canMove);
    }

    // Update is called once per frame
    void Update()
    {






    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(center, 0.3f);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("COLL ENTER");
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.name=="Wall")
    //    Debug.Log("TRIG ENTER");
    //}


}
