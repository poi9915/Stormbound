using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NavMeshAutoBake : MonoBehaviour
{
    void Start()
    {
        var surface = GetComponent<NavMeshSurface>();
        if (surface != null)
        {
            surface.BuildNavMesh(); // tự động bake khi chạy game
        }
    }
}
