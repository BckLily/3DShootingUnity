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



    // Start is called before the first frame update
    void Start()
    {

        // Transform ��ġ ����.
        tr = GetComponent<Transform>();



    }

    // Update is called once per frame
    void Update()
    {


        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        h_rotate = Input.GetAxis("Mouse X");
        //v_rotate = Input.GetAxis("Mouse Y");

        // ���� �¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * vertical) + (Vector3.right * horizontal);

        // Translate(�̵� ���� * �ӵ� * ���� �� * Time.deltaTime, ���� ��ǥ)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        // Vector3.up ���� �������� rotSpeed ��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h_rotate);





    }


}
