using UnityEngine;

public class TentaclePaintCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // Only continue if the object is tagged "Paintable"
        if (!other.CompareTag("Paintable")) return;

        // Find the PaintManager on the object or its parent
        var paintMgr = other.GetComponentInParent<PaintManager>();
        if (paintMgr != null)
        {
            paintMgr.DrawAtPoint(transform.position, other.gameObject);
        }
    }
}