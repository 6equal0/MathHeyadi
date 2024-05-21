using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        // 마우스의 스크린 좌표를 받아옴
        Vector3 mousePosition = Input.mousePosition;

        // 마우스의 스크린 좌표를 월드 좌표로 변환, 카메라의 z 값을 0으로 설정
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z));

        // 오브젝트의 위치와 마우스 월드 좌표 사이의 벡터를 계산
        Vector3 direction = target - transform.position;

        // 방향 벡터의 z 값을 0으로 설정하여 2D 평면에서의 회전을 고려
        direction.z = 0;

        // 방향 벡터를 기준으로 회전 각도를 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 오브젝트의 회전을 설정
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
