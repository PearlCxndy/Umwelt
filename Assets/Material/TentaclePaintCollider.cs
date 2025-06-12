using UnityEngine;

public class TentaclePaintCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Paintable"))
        {
            Renderer rend = other.GetComponent<Renderer>();
            if (rend != null)
            {
                Vector3 hitPoint = transform.position;
                PaintManager.Instance.DrawAtPoint(hitPoint, other.gameObject);
            }
        }
    }
}