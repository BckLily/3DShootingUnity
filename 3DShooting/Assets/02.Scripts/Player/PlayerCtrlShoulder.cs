using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlShoulder : MonoBehaviour
{
    // Player�� ����� ī�޶�
    public Camera camera;
    // Camera�� Transform
    Transform cameraTr;
    // Camera�� FollowCamTest Component
    FollowCamShoulder followCam;


    // Player�� Transform�� ������ ����
    Transform tr;
    // Player�� �ٶ� ������ ������ ����
    Quaternion playerRot;
    // �÷��̾� �̵� �ӵ�
    float moveSpeed = 15f;


    // Mouse�� �ִ� ��ġ
    Vector3 mousePos;
    // ���콺 ���� �ӵ�
    float mouseFPS = 3f;
    // Ű���� ���� �ӵ�
    float keyboardFPS = 3f;

    // ���� ����
    public GameObject weapon;
    // ���Ⱑ ������ ��ġ
    Transform weaponPos;


    // Start is called before the first frame update
    void Start()
    {
        // ���� ������Ʈ�� Transform�� ������ ����
        tr = this.GetComponent<Transform>();

        // MainCamera���� Camera Component�� ã�´�.
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        // MainCamera���� Transform Component�� ã�´�.
        cameraTr = camera.gameObject.GetComponent<Transform>();
        // followCam Component�� ����
        followCam = camera.GetComponentInParent<FollowCamShoulder>();


        // ���� ������Ʈ�� ���� ��Ҹ� ����
        weaponPos = tr.Find("PlayerCapsule").Find("WeaponPos").GetComponentInChildren<Transform>();
        // weaponPos�� �ڽ����� weapon�� ����.
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
        // ���콺 FPS ��ŭ�� ��� �ð��� �д�.
        yield return new WaitForSeconds(Time.deltaTime * mouseFPS);

        // ���� ���콺 ��ġ�� �޾ƿ´�.
        mousePos = Input.mousePosition;

        // Raycast�� �ε����� ��Ҹ� ������ ������ Ray�� �����ϴ� ����
        RaycastHit hit;
        // ���� ���콺�� �ִ� ��ġ���� Ray�� �����Ѵ�.
        Ray ray = camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
        {
            // ������ ��ġ�� ��� ������ ����.

            // Player�� ���콺�� ���� �ִ� ��ġ�� �ٶ󺸰� �Ѵ�.
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
