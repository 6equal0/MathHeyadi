using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight2D : MonoBehaviour
{
    Vector2 playerTarget;
    [SerializeField] public bool findTarget = false;   // 플레이어 오브젝트 발견 여부
    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f;
    [SerializeField] private float m_viewRadius = 1f;
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f;  // 추후 적 오브젝트가 바라보는 각도 입력해주면 될듯

    [SerializeField] private LayerMask m_viewTargetMask;
    [SerializeField] private LayerMask m_viewObstacleMask;

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;

    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }

    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle >= m_horizontalViewAngle) // 범위 안에 아무것도 없을 때
            {
                findTarget = false;
            }
            else if (angle <= m_horizontalViewHalfAngle && hitedTarget.CompareTag("Player")) // 범위 안에 Player 태그를 가진 오브젝트가 있을 때
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask);
                if (rayHitedTarget)
                {
                    // 벽이 목표물보다 앞에 있는지 확인
                    float distanceToObstacle = rayHitedTarget.distance;
                    float distanceToTarget = Vector2.Distance(originPos, targetPos);

                    if (distanceToObstacle < distanceToTarget)
                    {
                        if (m_bDebugMode)
                        {
                            Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                        }
                        findTarget = false;
                    }
                    else
                    {
                        hitedTargetContainer.Add(hitedTarget);
                        if (m_bDebugMode)
                        {
                            Debug.DrawLine(originPos, targetPos, Color.red);
                            playerTarget = targetPos;
                            findTarget = true;
                        }
                    }
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);
                    if (m_bDebugMode)
                    {
                        Debug.DrawLine(originPos, targetPos, Color.red);
                        playerTarget = targetPos;
                        findTarget = true;
                    }
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }

    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);

            FindViewTargets();
        }
    }

    IEnumerator ToPlayer()
    {
        while (findTarget)
        {
            yield return new WaitForSeconds(0.2f);
            Vector2 ene = gameObject.transform.position;
            ene = playerTarget - ene;
            gameObject.transform.position = ene;
        }
    }
}