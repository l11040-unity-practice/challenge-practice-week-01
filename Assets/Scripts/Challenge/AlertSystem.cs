using System;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    // fov가 45라면 45도 각도안에 있는 aesteriod를 인식할 수 있음.
    [SerializeField] private float fov = 45f;
    private float cosFov;
    // radius가 10이라면 반지름 10 범위에서 aesteriod들을 인식할 수 있음.
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    private void Start()
    {
        animator = GetComponent<Animator>();
        // FOV를 라디안으로 변환하고 코사인 값을 계산
        cosFov = MathF.Cos(fov * Mathf.Deg2Rad / 2);
    }

    private void Update()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        LayerMask mask = LayerMask.GetMask("Aesteriod");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, mask);

        if (targets.Length == 0)
        {
            animator.SetBool(blinking, false);
            return;
        }

        foreach (var target in targets)
        {
            Vector2 directionToTarget = target.transform.position - transform.position;
            directionToTarget.Normalize();

            Vector2 forward = transform.up;
            float dotProduct = Vector2.Dot(forward, directionToTarget);
            if (dotProduct >= cosFov)
            {
                animator.SetBool(blinking, true);
                return;
            }
        }

        animator.SetBool(blinking, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Vector3 fovLine1 = Quaternion.Euler(0, 0, fov / 2) * transform.up * radius;
        Vector3 fovLine2 = Quaternion.Euler(0, 0, -fov / 2) * transform.up * radius;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }
}