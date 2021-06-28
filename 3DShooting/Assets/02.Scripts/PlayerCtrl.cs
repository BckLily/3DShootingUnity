using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Camera camera;

    // Player의 Transform을 저장할 변수
    private Transform tr;
    // Player가 바라볼 방향을 저장할 변수
    Vector3 playerRot;

    // Mouse가 있는 위치
    Vector3 mousePos;

    // 마우스 반응 속도
    float mouseFPS = 3f;
    // 키보드 반응 속도
    float keyboardFPS = 3f;
    // 플레이어 이동 속도
    float moveSpeed = 15f;

    // 무기가 저장될 변수
    public GameObject weapon;
    // 무기가 생성될 위치
    //[SerializeField]
    Transform weaponPos;



    // Start is called before the first frame update
    void Start()
    {
        tr = this.GetComponent<Transform>();
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();

        weaponPos = tr.Find("PlayerCapsule").Find("WeaponPos").GetComponent<Transform>();
        Instantiate(weapon, weaponPos);

    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(LookingMousePos());
        StartCoroutine(KeyBoardControl());
    }

    IEnumerator LookingMousePos()
    {
        yield return new WaitForSeconds(Time.deltaTime * mouseFPS);

        mousePos = Input.mousePosition;
        // 현재 마우스 위치 받아온다. (이렇게 받아오면 2차원 값(화면을 기준으로 변화되는 X, Y 값)이 입력된다.)

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(mousePos);

        //if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
        {
            //Transform objectHit = hit.transform;
            //Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        // Physics.Raycast(ray(마우스 위치), out hit(마우스가 닿는 부분), Mathf.Infinity(최대 Ray거리), 1 << 6(Tag::FLOOR))
        // 를 하면 마우스가 floor에 닿았을 때의 위치를 알 수 있다.

        // 닿은 위치는 x, 0 , z 가 될 건데, 이 값들을 사용해서 tangent 계산을 한다.??
        //var yRot = Mathf.Tan((hit.point.x - tr.position.x) / (hit.point.z - tr.position.z));
        //var yRot = 
        //Debug.Log("yRot: " + yRot);
        //tr.rotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        // 그냥 해당 오브젝트 Transform.LookAt(Vector3) 박으면 그 방향으로 회전한다.
        tr.LookAt(hit.point);

        //Debug.DrawRay(tr.position, hit.point, Color.red);
        //Debug.Log("tr.Rot: " + tr.rotation);

    }


    IEnumerator KeyBoardControl()
    {
        yield return new WaitForSeconds(Time.deltaTime * keyboardFPS);

        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.z -= transform.position.z;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x += transform.position.x;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x -= transform.position.x;
        }

        tr.position += movement.normalized * Time.deltaTime * moveSpeed;



    }

}
