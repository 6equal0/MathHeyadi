using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform playerTransform; // �÷��̾��� Transform ����
    public int maxReflections = 3;
    public float maxRayDistance = 100f;
    public LayerMask layerDetection;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        Vector3 startPosition = playerTransform.position; // �������� ���� ��ġ�� �÷��̾� ��ġ�� ����
        Vector3 direction = playerTransform.right; // �������� ������ �÷��̾ �ٶ󺸴� �������� ����

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPosition);

        float remainingDistance = maxRayDistance;
        int reflections = 0;

        while (remainingDistance > 0 && reflections < maxReflections)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, remainingDistance, layerDetection);
            if (hit.collider != null)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                remainingDistance -= Vector2.Distance(startPosition, hit.point);
                startPosition = hit.point;
                direction = Vector2.Reflect(direction, hit.normal);

                reflections++;

                if (reflections >= maxReflections)
                {
                    // ������ �ݻ縦 ó���� �� ����
                    RaycastHit2D finalHit = Physics2D.Raycast(startPosition, direction, remainingDistance, layerDetection);
                    if (finalHit.collider != null)
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, finalHit.point);
                    }
                    else
                    {
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition + direction * remainingDistance);
                    }
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition + direction * remainingDistance);
                break;
            }
        }
    }
}
