using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * 카메라가 플레이어를 따라다니려면
 * 1. 플레이어 오브젝트 위치 파악
 * 2. 카메라의 위치 설정
 * 3. 카메라가 플레이어 오브젝트를 바라보도록 설정
 * 4. 고정된 방향으로 플레이어를 볼 수 있게 플레이어가 이동하면
 *    같이 움직이여한다.
 * 
 */
public class FollowCam : MonoBehaviour
{
    // Player GameObject
    public GameObject player;
    // PlayerTr
    Transform playerTr;
    // 플레이어 게임 오브젝트 생성할 위치
    public Vector3 makePlayerPos = Vector3.zero;


    // 카메라 Rot(camera obj)
    Quaternion camRot;
    // 카메라 Tr (box obj)
    Transform camTr;
    // 카메라의 상대적 위치
    [SerializeField]
    Vector3 camPos = new Vector3(0f, 0f, 0f);
    // 카메라가 플레이어의 위치보다 조금 더 앞을 보게 만든다.

    float camSpeed = 5f;

    // 카메라 높이 (고정치)
    // 플레이어의 높이보다 항상 20f 높다.
    float camHeight = 20f;
    // cameraBox가 보게될 방향 (플레이어 위치 + 높이20)


    Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, makePlayerPos, Quaternion.identity);

        // 플레이어의 위치를 받아오는 건데
        // 나중에 플레이어를 prefab으로 만들게 되면
        // public으로 처리해서 player를 0,0,0 위치에 생성을 하고, (원하는 위치에)
        // 플레이어를 가져오는게 오류가 발생하지 않으려면 좋을 듯.
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
            // Rotate >> 회전
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
