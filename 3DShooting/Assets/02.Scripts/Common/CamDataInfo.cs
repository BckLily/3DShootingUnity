using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamDataInfo
{

    public class ShoulderView
    {

        public float distance = 5.0f;       // 추적할 대상과의 거리
        public float height = 2.25f;         // 추적할 대상으로부터 높이
        public float targetOffset = 1.625f;   // 추적 좌표의 오프셋

    }


    public class TopView
    {

        public float distance = 4.0f;       // 추적할 대상과의 거리
        public float height = 20f;         // 추적할 대상으로부터 높이
        public float targetOffset = 8f;   // 추적 좌표의 오프셋

    }



}
