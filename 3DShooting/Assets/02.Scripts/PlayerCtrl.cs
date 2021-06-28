using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public Camera camera;

    // Player�� Transform�� ������ ����
    private Transform tr;
    // Player�� �ٶ� ������ ������ ����
    Vector3 playerRot;

    // Mouse�� �ִ� ��ġ
    Vector3 mousePos;

    // ���콺 ���� �ӵ�
    float mouseFPS = 3f;
    // Ű���� ���� �ӵ�
    float keyboardFPS = 3f;
    // �÷��̾� �̵� �ӵ�
    float moveSpeed = 15f;

    // ���Ⱑ ����� ����
    public GameObject weapon;
    // ���Ⱑ ������ ��ġ
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
        // ���� ���콺 ��ġ �޾ƿ´�. (�̷��� �޾ƿ��� 2���� ��(ȭ���� �������� ��ȭ�Ǵ� X, Y ��)�� �Էµȴ�.)

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(mousePos);

        //if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
        {
            //Transform objectHit = hit.transform;
            //Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            //Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        // Physics.Raycast(ray(���콺 ��ġ), out hit(���콺�� ��� �κ�), Mathf.Infinity(�ִ� Ray�Ÿ�), 1 << 6(Tag::FLOOR))
        // �� �ϸ� ���콺�� floor�� ����� ���� ��ġ�� �� �� �ִ�.

        // ���� ��ġ�� x, 0 , z �� �� �ǵ�, �� ������ ����ؼ� tangent ����� �Ѵ�.??
        //var yRot = Mathf.Tan((hit.point.x - tr.position.x) / (hit.point.z - tr.position.z));
        //var yRot = 
        //Debug.Log("yRot: " + yRot);
        //tr.rotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        // �׳� �ش� ������Ʈ Transform.LookAt(Vector3) ������ �� �������� ȸ���Ѵ�.
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
