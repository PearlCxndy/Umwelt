using UnityEngine;

public class PivotCenterer : MonoBehaviour
{

    
    void Start()
    {
        CenterPivot();
    }

    void CenterPivot()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return;

        Bounds combinedBounds = renderers[0].bounds;
        foreach (Renderer rend in renderers)
        {
            combinedBounds.Encapsulate(rend.bounds);
        }

        Vector3 center = combinedBounds.center;
        Vector3 offset = transform.position - center;

        // Move all children relative to new pivot
        foreach (Transform child in transform)
        {
            child.position += offset;
        }

        // Move parent to new center
        transform.position = center;
    }
}
