using UnityEngine;

public class TentaclePaintCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Paintable"))
        {
            PaintManager.Instance.DrawAtPoint(transform.position, other.gameObject);
        }
    }
}