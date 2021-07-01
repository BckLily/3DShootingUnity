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



    // Start is called before the first frame update
    void Start()
    {

        // Transform 위치 저장.
        tr = GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update()
    {


        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        h_rotate = Input.GetAxis("Mouse X");
        //v_rotate = Input.GetAxis("Mouse Y");

        // 전후 좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(이동 방향 * 속도 * 변위 값 * Time.deltaTime, 기준 좌표)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up 축을 기준으로 rotSpeed 만큼의 속도로 회전
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h_rotate);





    }


}
