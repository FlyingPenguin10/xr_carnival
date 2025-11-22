using UnityEngine;

public class PatrolWaypoint : MonoBehaviour
{
    public Color gizmoColor = Color.cyan;
    public float gizmoSize = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 2f);
    }
}
