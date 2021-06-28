using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * ī�޶� �÷��̾ ����ٴϷ���
 * 1. �÷��̾� ������Ʈ ��ġ �ľ�
 * 2. ī�޶��� ��ġ ����
 * 3. ī�޶� �÷��̾� ������Ʈ�� �ٶ󺸵��� ����
 * 4. ������ �������� �÷��̾ �� �� �ְ� �÷��̾ �̵��ϸ�
 *    ���� �����̿��Ѵ�.
 * 
 */
public class FollowCam : MonoBehaviour
{
    // Player GameObject
    public GameObject player;
    // PlayerTr
    Transform playerTr;
    // �÷��̾� ���� ������Ʈ ������ ��ġ
    public Vector3 makePlayerPos = Vector3.zero;


    // ī�޶� Rot(camera obj)
    Quaternion camRot;
    // ī�޶� Tr (box obj)
    Transform camTr;
    // ī�޶��� ����� ��ġ
    [SerializeField]
    Vector3 camPos;
    // 

    float camSpeed = 5f;

    // ī�޶� ���� (����ġ)
    // �÷��̾��� ���̺��� �׻� 20f ����.
    float camHeight = 20f;
    // cameraBox�� ���Ե� ���� (�÷��̾� ��ġ + ����20)


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, makePlayerPos, Quaternion.identity);

        // �÷��̾��� ��ġ�� �޾ƿ��� �ǵ�
        // ���߿� �÷��̾ prefab���� ����� �Ǹ�
        // public���� ó���ؼ� player�� 0,0,0 ��ġ�� ������ �ϰ�, (���ϴ� ��ġ��)
        // �÷��̾ �������°� ������ �߻����� �������� ���� ��.
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        camRot = Quaternion.Euler(new Vector3(60, 45, 0));

        //camTr = transform.parent.GetComponent<Transform>();
        transform.rotation = camRot;
        camPos = new Vector3(-10f, 9f, -10f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //var cameraMove = Vector3.zero;

        //// text Code
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    //transform.RotateAround(transform.position, transform.up, 1f);
        //    //transform.Rotate(transform.position, 1f);
        //    // Rotate >> ȸ��
        //    //cameraMove.z += transform.localPosition.z;
        //    cameraMove.x += transform.localPosition.x;
        //}
        //else if (Input.GetKey(KeyCode.E))
        //{
        //    //transform.RotateAround(transform.position, -transform.up , 1f);
        //    //transform.Rotate(transform.position, -1f);
        //    //cameraMove.z -= transform.localPosition.z;
        //    cameraMove.x = transform.localPosition.x;
        //}

        //camPos += cameraMove.normalized * Time.deltaTime * camSpeed;
        //camPos = camPos.normalized;



        // use Code

        transform.position = playerTr.position + camPos;

        transform.LookAt(playerTr);




    }
}
