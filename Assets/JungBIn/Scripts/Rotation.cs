using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        // ���콺�� ��ũ�� ��ǥ�� �޾ƿ�
        Vector3 mousePosition = Input.mousePosition;

        // ���콺�� ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ, ī�޶��� z ���� 0���� ����
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z));

        // ������Ʈ�� ��ġ�� ���콺 ���� ��ǥ ������ ���͸� ���
        Vector3 direction = target - transform.position;

        // ���� ������ z ���� 0���� �����Ͽ� 2D ��鿡���� ȸ���� ���
        direction.z = 0;

        // ���� ���͸� �������� ȸ�� ������ ���
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������Ʈ�� ȸ���� ����
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
