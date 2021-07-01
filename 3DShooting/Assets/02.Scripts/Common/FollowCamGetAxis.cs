using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CamDataInfo;

public class FollowCamGetAxis : MonoBehaviour
{
    
    public Transform target;            // ������ ���
    public float moveDamping = 10.0f;   // �̵� �ӵ� ���
    public float rotateDamping = 10.0f; // ȸ�� �ӵ� ���
    public float distance = 5.0f;       // ������ ������ �Ÿ�
    public float height = 2.25f;         // ������ ������κ��� ����
    public float targetOffset = 1.625f;   // ���� ��ǥ�� ������
    public Vector3 targetMakePos = Vector3.zero; // ������ ����� ������ ��ġ

    Camera camera;                      // ī�޶� ������Ʈ�� ����
    

    // Camera�� transform
    private Transform tr;


    // Start is called before the first frame update
    void Start()
    {

        tr = GetComponent<Transform>();
        camera = tr.Find("MainCamera").GetComponent<Camera>();

        // ������ ����� ����.
        var target_obj = Instantiate(target, targetMakePos, Quaternion.identity);
        target = target_obj.transform;

        ShoulderView view = new ShoulderView();
        distance = view.distance;
        height = view.height;
        targetOffset = view.targetOffset;

        camera.orthographic = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShoulderView view = new ShoulderView();
            distance = view.distance;
            height = view.height;
            targetOffset = view.targetOffset;

            camera.orthographic = false;

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TopView view = new TopView();
            distance = view.distance;
            height = view.height;
            targetOffset = view.targetOffset;

            camera.orthographic = true;
        }

 
    }


    private void LateUpdate()
    {

        // ī�޶��� ���� �� �Ÿ��� ���
        // Ÿ���� ��ġ / Ÿ�����κ��� ���� �Ÿ� / Ÿ�����κ��� ����
        var camPos = target.position - (target.forward * distance) + (target.up * height);

        // ī�޶��� ��ġ �̵� �ε巴�� �̵��ϰ� �Ѵ�.
        // ���� ��ġ / ���� ��ġ / �̵��� �ӵ�
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);

        // ī�޶��� ȸ�� �ε巴�� �̵��ϰ� �Ѵ�.
        // ���� ���� / ���� ���� / �̵��� �ӵ�
        // target�� rotation�� ������ ���ϸ� �����ϸ� �ٲ..?
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);


        // ī�޶� ���� ����� ������ ȸ�� ��Ų��.
        tr.LookAt(target.position + (target.up * targetOffset));


    }


}
