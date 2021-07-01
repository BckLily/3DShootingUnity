using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CamDataInfo;

public class FollowCamGetAxis : MonoBehaviour
{
    
    public Transform target;            // 추적할 대상
    public float moveDamping = 10.0f;   // 이동 속도 계수
    public float rotateDamping = 10.0f; // 회전 속도 계수
    public float distance = 5.0f;       // 추적할 대상과의 거리
    public float height = 2.25f;         // 추적할 대상으로부터 높이
    public float targetOffset = 1.625f;   // 추적 좌표의 오프셋
    public Vector3 targetMakePos = Vector3.zero; // 추적할 대상을 생성할 위치

    Camera camera;                      // 카메라 컴포넌트를 저장
    

    // Camera의 transform
    private Transform tr;


    // Start is called before the first frame update
    void Start()
    {

        tr = GetComponent<Transform>();
        camera = tr.Find("MainCamera").GetComponent<Camera>();

        // 추적할 대상을 생성.
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

        // 카메라의 높이 및 거리를 계산
        // 타겟의 위치 / 타겟으로부터 뒤쪽 거리 / 타겟으로부터 높이
        var camPos = target.position - (target.forward * distance) + (target.up * height);

        // 카메라의 위치 이동 부드럽게 이동하게 한다.
        // 현재 위치 / 변경 위치 / 이동할 속도
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);

        // 카메라의 회전 부드럽게 이동하게 한다.
        // 현재 각도 / 변경 각도 / 이동할 속도
        // target의 rotation이 무기의 상하를 조절하면 바뀌나..?
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);


        // 카메라가 추적 대상을 보도록 회전 시킨다.
        tr.LookAt(target.position + (target.up * targetOffset));


    }


}
