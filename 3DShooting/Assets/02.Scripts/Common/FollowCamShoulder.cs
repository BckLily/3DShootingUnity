using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 1. 플레이어 위치 파악
 * 2. 플레이어 따라 이동
 * 3. 플레이어 위치 쳐다보기
 * 4. 현재 Cam의 Rot 값 저장.
 * 5. Q, E로 회전시 Rot 값 저장과정 건너뛰고
 *    (생각해보니 저장하는 과정에서 조건 따로 필요없이
 *    그냥 돌아올 때 적절한 속도로 움직이게 하면 될 듯...?)
 *    회전 끝났을 때 Rot 값으로 복귀
 *    복귀 속도 설정.
 */

public class FollowCamShoulder : MonoBehaviour
{
    // Player GameObject Prefab
    public GameObject player;
    // Player Transform 사용할 값.
    Transform playerTr;

    // Player Game Object 생성 위치
    public Vector3 playerMakePos = Vector3.zero;


    // 현재 카메라 Rot(회전 미적용)
    public Quaternion saveCamRot;
    // 카메라의 상대적 위치(캐릭터와의 거리)
    // 줌인 줌아웃에 사용한다.
    [SerializeField]
    Vector3 camOffset = Vector3.zero;
    // 카메라가 플레이어의 위치보다 조금 높은 곳을 보게 한다.
    [SerializeField]
    Vector3 lookatCamUp;
    [SerializeField]
    float offSetDist;


    // 카메라의 이동 속도??
    // 부드럽게 움직인다거나 할 필요가 있으면 사용
    float camSpeed = 5f;
    // 카메라의 회전 속도
    float camDampingSpeed = 5f;
    // 카메라의 돌아오는 회전 속도
    float camRollBackDampingSpeed = 8f;


    // 줌인 줌 아웃이나 회전에 사용할 값.
    // 현재 줌 값
    public float currZoom = 7f;
    // 최대 줌인 값
    public float minZoom = 4.5f;
    // 최대 줌 아웃 값
    public float maxZoom = 13f;


    // Study Project / Book Project 참고해서 처리해야 할 듯.

    // Start is called before the first frame update
    void Start()
    {
        // Player Object 생성. playerMakePos의 위치에 Quaternion.identity 방향으로
        var player_use = Instantiate(player, playerMakePos, Quaternion.identity);

        playerTr = player_use.GetComponent<Transform>();

        camOffset = new Vector3(0f, 0.5f, -1.25f);
        lookatCamUp = new Vector3(transform.localPosition.x * 1.25f, 2f, 0f);

        

        offSetDist = camOffset.magnitude;

        camOffset.Normalize();


    }

    void Update()
    {

        // 마우스 휠로 줌인 줌 아웃
        currZoom -= Input.GetAxis("Mouse ScrollWheel");
        // 줌 최대 최소 값 설정
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





        // camera Box의 위치를 player의 Transform + camOffset으로 설정한다.
        // 이후 줌인 줌아웃이 생기면 camOffset 값을 조금 변경하거나
        // 배수 값을 이용해서 적절한 위치에 있을 수 있게 한다.
        transform.position = playerTr.position + camOffset * currZoom * offSetDist;

        transform.rotation = Quaternion.Slerp(transform.rotation, playerTr.rotation, Time.deltaTime * camDampingSpeed);

        var lookVector = new Vector3(playerTr.position.x + lookatCamUp.x, playerTr.position.y + lookatCamUp.y, playerTr.position.z + lookatCamUp.z);

        transform.LookAt(lookVector);

        //var mouseDir = Input.GetAxis("Mouse X");

        //transform.Rotate(Vector3.up, mouseDir);

    }
}
