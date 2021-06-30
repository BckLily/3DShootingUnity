using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlShoulder : MonoBehaviour
{
    // Player를 따라올 카메라
    public Camera camera;
    // Camera의 Transform
    Transform cameraTr;
    // Camera의 FollowCamTest Component
    FollowCamShoulder followCam;


    // Player의 Transform을 저장할 변수
    Transform tr;
    // Player가 바라볼 방향을 저장할 변수
    Quaternion playerRot;
    // 플레이어 이동 속도
    float moveSpeed = 15f;


    // Mouse가 있는 위치
    Vector3 mousePos;
    // 마우스 반응 속도
    float mouseFPS = 3f;
    // 키보드 반응 속도
    float keyboardFPS = 3f;

    // 무기 변수
    public GameObject weapon;
    // 무기가 생성될 위치
    Transform weaponPos;


    // Start is called before the first frame update
    void Start()
    {
        // 현재 오브젝트의 Transform를 변수에 저장
        tr = this.GetComponent<Transform>();

        // MainCamera에서 Camera Component를 찾는다.
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        // MainCamera에서 Transform Component를 찾는다.
        cameraTr = camera.gameObject.GetComponent<Transform>();
        // followCam Component를 저장
        followCam = camera.GetComponentInParent<FollowCamShoulder>();


        // 무기 오브젝트가 있을 장소를 설정
        weaponPos = tr.Find("PlayerCapsule").Find("WeaponPos").GetComponentInChildren<Transform>();
        // weaponPos의 자식으로 weapon을 생성.
        Instantiate(weapon, weaponPos);
    }



    // Update is called once per frame
    void Update()
    {
        playerRot = followCam.saveCamRot;

        StartCoroutine(LookingMousePos());
        StartCoroutine(KeyBoardControl());
    }

    IEnumerator LookingMousePos()
    {
        // 마우스 FPS 만큼의 대기 시간을 둔다.
        yield return new WaitForSeconds(Time.deltaTime * mouseFPS);

        // 현재 마우스 위치를 받아온다.
        mousePos = Input.mousePosition;

        // Raycast가 부딪히는 장소를 저장할 변수와 Ray를 저장하는 변수
        RaycastHit hit;
        // 현재 마우스가 있는 위치에서 Ray가 시작한다.
        Ray ray = camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
        {
            // 조건이 일치할 경우 실행할 구문.

            // Player가 마우스가 현재 있는 위치를 바라보게 한다.
            tr.LookAt(hit.point);
        }
    }


    IEnumerator KeyBoardControl()
    {
        yield return new WaitForSeconds(Time.deltaTime * keyboardFPS);

        var movement = Vector3.zero;
        Vector3 moveRot = playerRot.ToEulerAngles();

        moveRot.y = 0f;

        //Debug.Log(moveRot);

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
            //movement += (transform.localRotation * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
            //movement += (transform.localRotation * Vector3.back);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            //movement += (transform.localRotation * Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            //movement += (transform.localRotation * Vector3.right);
        }

        Debug.Log(movement);

        tr.Translate(movement.normalized * Time.deltaTime * moveSpeed, Space.World);

    }



}
