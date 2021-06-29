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
    Vector3 camPos = new Vector3(0f, 0f, 0f);
    // ī�޶� �÷��̾��� ��ġ���� ���� �� ���� ���� �����.

    float camSpeed = 5f;

    // ī�޶� ���� (����ġ)
    // �÷��̾��� ���̺��� �׻� 20f ����.
    float camHeight = 20f;
    // cameraBox�� ���Ե� ���� (�÷��̾� ��ġ + ����20)


    Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, makePlayerPos, Quaternion.identity);

        // �÷��̾��� ��ġ�� �޾ƿ��� �ǵ�
        // ���߿� �÷��̾ prefab���� ����� �Ǹ�
        // public���� ó���ؼ� player�� 0,0,0 ��ġ�� ������ �ϰ�, (���ϴ� ��ġ��)
        // �÷��̾ �������°� ������ �߻����� �������� ���� ��.
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        //camRot = Quaternion.Euler(new Vector3(60, 45, 0));

        //camTr = transform.parent.GetComponent<Transform>();
        transform.rotation = camRot;
        offset = new Vector3(-6f, 3f, -6f);
        camPos = new Vector3(0f, 3f, 0f);
        //transform.position = playerTr.position + offset;
        //transform.LookAt(playerTr);

    }

    // Update is called once per frame
    void Update()
    {        
        // text Code
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(playerTr.position, Vector3.up, 1f);

            offset = transform.position - playerTr.position;
            offset.Normalize();

            //transform.Rotate(transform.position, 1f);
            // Rotate >> ȸ��
            //cameraMove.z += transform.localPosition.z;
            //cameraMove.x += transform.localPosition.x;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(playerTr.position, Vector3.up, -1f);

            offset = transform.position - playerTr.position;
            offset.Normalize();

            //transform.Rotate(transform.position, -1f);
            //cameraMove.z -= transform.localPosition.z;
            //cameraMove.x = transform.localPosition.x;
        }

    }

    private void LateUpdate()
    {
        var cameraMove = Vector3.zero;

        //camPos += cameraMove.normalized * Time.deltaTime * camSpeed;
        //camPos = camPos.normalized;



        // use Code
        // Test Code
        transform.position = playerTr.position + offset * 2f;
        //transform.position = playerTr.position + camPos;

        Vector3 lookVector = new Vector3(playerTr.position.x + camPos.x, playerTr.position.y + camPos.y, playerTr.position.z + camPos.z);

        transform.LookAt(lookVector);
        Debug.Log("lookVector: " + lookVector);
        //Debug.Log("position: " + lookVector.position);
        Debug.Log("playerTr: " + playerTr);
        Debug.Log("position: " + playerTr.position);





    }
}
