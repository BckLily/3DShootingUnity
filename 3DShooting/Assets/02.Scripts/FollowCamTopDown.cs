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



public class FollowCamTopDown : MonoBehaviour
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


    // ī�޶��� �̵� �ӵ�??
    // �ε巴�� �����δٰų� �� �ʿ䰡 ������ ���
    float camSpeed = 5f;
    // ī�޶��� ȸ�� �ӵ�
    float camDampingSpeed = 5f;
    // ī�޶��� ���ƿ��� ȸ�� �ӵ�
    float camRollBackDampingSpeed = 8f;


    //// ���� �� �ƿ��̳� ȸ���� ����� ��.
    //Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // Player Object ����. playerMakePos�� ��ġ�� Quaternion.identity ��������
        var player_use = Instantiate(player, playerMakePos, Quaternion.identity);

        playerTr = player_use.GetComponent<Transform>();

        camOffset = new Vector3(0f, 45f, -20f);
        lookatCamUp = Vector3.up * 3f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {

        }
        else if (Input.GetKey(KeyCode.E))
        {

        }
        else
        {
            saveCamRot = gameObject.transform.rotation;
        }


    }

    private void LateUpdate()
    {
        // camera Box�� ��ġ�� player�� Transform + camOffset���� �����Ѵ�.
        // ���� ���� �ܾƿ��� ����� camOffset ���� ���� �����ϰų�
        // ��� ���� �̿��ؼ� ������ ��ġ�� ���� �� �ְ� �Ѵ�.
        transform.position = playerTr.position + camOffset;

        var lookVector = new Vector3(playerTr.position.x, playerTr.position.y + lookatCamUp.y, playerTr.position.z);

        transform.LookAt(lookVector);


    }

}
