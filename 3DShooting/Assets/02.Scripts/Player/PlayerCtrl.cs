using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 플레이어를 따라올 카메라.
    public Camera camera;
    // 카메라의 Transform
    Transform cameraTr;

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

        cameraTr = camera.gameObject.GetComponent<Transform>();

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

    /*
     * 플레이어의 이동이지만 잘 생각해보면 사실 카메라가 어디를 보고 있느냐에 따라
     * 정면이 달라진다.
     * 
     * 탑 뷰, 탑 다운의 경우 정면은 위쪽 방향이 될 것이고
     * 쿼터뷰의 경우 정면은 카메라가 보고 있는 대각선 방향이 될 것이다.
     * 숄더뷰의 경우도 플레이어와 카메라가 보통 같은 방향을 보고 있기 때문에
     * 정면은 카메라가 보고 있는 방향이 될 것이다.
     * 
     * 단, 카메라의 회전이 가능한 경우,
     * 이동 전의 카메라의 Rotation을 저장해두고 그 Rotation 값에 대해서
     * 플레이어의 정면이동을 해야할 것이다.
     * 이후 카메라 회전이 끝나면 저장해둔 값으로 다시 복구 시켜야 한다.
     * 
     */

    IEnumerator KeyBoardControl()
    {
        yield return new WaitForSeconds(Time.deltaTime * keyboardFPS);

        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }

        tr.Translate( movement * Time.deltaTime * moveSpeed);



    }

}
