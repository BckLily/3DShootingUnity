using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 1. �÷��̾� ��ġ �ľ�
 * 2. �÷��̾� ���� �̵�
 * 3. �÷��̾� ��ġ �Ĵٺ���
 * 4. ���� Cam�� Rot �� ����.
 * 5. Q, E�� ȸ���� Rot �� ������� �ǳʶٰ�
 *    (�����غ��� �����ϴ� �������� ���� ���� �ʿ����
 *    �׳� ���ƿ� �� ������ �ӵ��� �����̰� �ϸ� �� ��...?)
 *    ȸ�� ������ �� Rot ������ ����
 *    ���� �ӵ� ����.
 */

public class FollowCamShoulder : MonoBehaviour
{
    // Player GameObject Prefab
    public GameObject player;
    // Player Transform ����� ��.
    Transform playerTr;

    // Player Game Object ���� ��ġ
    public Vector3 playerMakePos = Vector3.zero;


    // ���� ī�޶� Rot(ȸ�� ������)
    public Quaternion saveCamRot;
    // ī�޶��� ����� ��ġ(ĳ���Ϳ��� �Ÿ�)
    // ���� �ܾƿ��� ����Ѵ�.
    [SerializeField]
    Vector3 camOffset = Vector3.zero;
    // ī�޶� �÷��̾��� ��ġ���� ���� ���� ���� ���� �Ѵ�.
    [SerializeField]
    Vector3 lookatCamUp;
    [SerializeField]
    float offSetDist;


    // ī�޶��� �̵� �ӵ�??
    // �ε巴�� �����δٰų� �� �ʿ䰡 ������ ���
    float camSpeed = 5f;
    // ī�޶��� ȸ�� �ӵ�
    float camDampingSpeed = 5f;
    // ī�޶��� ���ƿ��� ȸ�� �ӵ�
    float camRollBackDampingSpeed = 8f;


    // ���� �� �ƿ��̳� ȸ���� ����� ��.
    // ���� �� ��
    public float currZoom = 7f;
    // �ִ� ���� ��
    public float minZoom = 4.5f;
    // �ִ� �� �ƿ� ��
    public float maxZoom = 13f;


    // Study Project / Book Project �����ؼ� ó���ؾ� �� ��.

    // Start is called before the first frame update
    void Start()
    {
        // Player Object ����. playerMakePos�� ��ġ�� Quaternion.identity ��������
        var player_use = Instantiate(player, playerMakePos, Quaternion.identity);

        playerTr = player_use.GetComponent<Transform>();

        camOffset = new Vector3(0f, 0.5f, -1.25f);
        lookatCamUp = new Vector3(transform.localPosition.x * 1.25f, 2f, 0f);

        

        offSetDist = camOffset.magnitude;

        camOffset.Normalize();


    }

    void Update()
    {

        // ���콺 �ٷ� ���� �� �ƿ�
        currZoom -= Input.GetAxis("Mouse ScrollWheel");
        // �� �ִ� �ּ� �� ����
        currZoom = Mathf.Clamp(currZoom, minZoom, maxZoom);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(playerTr.position, Vector3.up, camDampingSpeed);
            camOffset = transform.position - playerTr.position;
            camOffset.Normalize();


            
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(playerTr.position, Vector3.up, -camDampingSpeed);
            camOffset = transform.position - playerTr.position;
            camOffset.Normalize();


        }
        else
        {
            saveCamRot = gameObject.transform.rotation;
        }





        // camera Box�� ��ġ�� player�� Transform + camOffset���� �����Ѵ�.
        // ���� ���� �ܾƿ��� ����� camOffset ���� ���� �����ϰų�
        // ��� ���� �̿��ؼ� ������ ��ġ�� ���� �� �ְ� �Ѵ�.
        transform.position = playerTr.position + camOffset * currZoom * offSetDist;

        transform.rotation = Quaternion.Slerp(transform.rotation, playerTr.rotation, Time.deltaTime * camDampingSpeed);

        var lookVector = new Vector3(playerTr.position.x + lookatCamUp.x, playerTr.position.y + lookatCamUp.y, playerTr.position.z + lookatCamUp.z);

        transform.LookAt(lookVector);

        //var mouseDir = Input.GetAxis("Mouse X");

        //transform.Rotate(Vector3.up, mouseDir);

    }
}
