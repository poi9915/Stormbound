using UnityEngine;

public class BaseVisualizer : MonoBehaviour
{
    public float priorityRange = 3f; // vùng xung quanh base

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , priorityRange);
    }
}
