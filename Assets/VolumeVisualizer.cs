using UnityEngine;

[ExecuteAlways]
public class VolumeVisualizer : MonoBehaviour
{
    public Color outlineColor = Color.cyan;

    void OnDrawGizmos()
    {
        Gizmos.color = outlineColor;
        Gizmos.matrix = transform.localToWorldMatrix;

        // Draw wire sphere with radius from SphereCollider
        SphereCollider sphere = GetComponent<SphereCollider>();
        if (sphere != null)
        {
            Gizmos.DrawWireSphere(sphere.center, sphere.radius);
        }
    }
}
