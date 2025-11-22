using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshDebugger : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public bool showBounds = true;

    private void OnDrawGizmos()
    {
        if (navMeshSurface == null || !showBounds) return;

        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(
            navMeshSurface.transform.position + navMeshSurface.center,
            navMeshSurface.transform.rotation,
            Vector3.one
        );
        Gizmos.DrawWireCube(Vector3.zero, navMeshSurface.size);
    }
}
