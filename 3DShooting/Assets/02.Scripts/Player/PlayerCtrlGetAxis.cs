using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlGetAxis : MonoBehaviour
{

    private float horizontal = 0.0f;    // Player가 움직일 가로 값. (좌우)
    private float vertical = 0.0f;      // Player가 움직일 세로 값. (전후)
    private float h_rotate = 0.0f;      // player가 회전할 값. (좌우 회전)
    private float v_rotate = 0.0f;      // player가 회전할 값. (상하 회전)


    private Transform tr;               // Player의 Transform 
    private float moveSpeed = 10.0f;    // Player의 이동 속도
    private float rotSpeed = 120.0f;    // Player의 회전 속도

    private Rigidbody rb;               // Player의 Rigidbody
    bool canMove;                       // Player의 이동가능 여부

    Vector3 center;                     // Player의 중심부
    Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {

        // Transform 위치 저장.
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



        // 전후 좌우 이동 방향 벡터 계산
        moveDir = new Vector3(horizontal, 0, vertical);
        //Vector3 moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);


        var sphereCast = Physics.SphereCastAll(center, 0.35f, Vector3.up, 0f, 1 << 7);
        //Physics.SphereCast(center, 0.35f, Vector3.up, 0, 1<<7);

        if (moveDir == Vector3.zero)
        {
            canMove = false;
        }
        else if(moveDir != Vector3.zero && sphereCast == null) // movdDir != 000 , 움직이려고 하고 있을 때, sphereCast == null, 충돌체가 없는 경우
        {
            // 이동할 수 있게 해준다.
            canMove = true;
        }



        if (canMove)
        {

            // Translate(이동 방향 * 속도 * 변위 값 * Time.deltaTime, 기준 좌표)
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        }

        // Vector3.up 축을 기준으로 rotSpeed 만큼의 속도로 회전
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
