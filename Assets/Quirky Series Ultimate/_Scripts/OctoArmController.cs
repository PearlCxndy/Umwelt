using UnityEngine;
using System.Collections;

public class TentacleMouseController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask groundLayer;
    public float moveSpeed = 10f;
    public float liftHeight = 2f;
    public bool invertX = false;
    public bool invertZ = false;

    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 dragStartMouseWorld;
    private Vector3 dragStartTargetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }

        if (isDragging)
        {
            DragTarget();
        }
    }

    void TryStartDrag()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f, groundLayer))
        {
            isDragging = true;
            rb.isKinematic = true;

            dragStartMouseWorld = GetMouseWorldPlane();
            dragStartTargetPos = transform.position;
        }
    }

    void DragTarget()
    {
        Vector3 currentMouseWorld = GetMouseWorldPlane();
        Vector3 offset = currentMouseWorld - dragStartMouseWorld;

        if (invertX) offset.x = -offset.x;
        if (invertZ) offset.z = -offset.z;

        Vector3 newPosition = dragStartTargetPos + new Vector3(offset.x, 0, offset.z);
        newPosition.y = dragStartTargetPos.y + liftHeight;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
    }

    void EndDrag()
    {
        isDragging = false;
        StartCoroutine(DelayedDrop());
    }

    IEnumerator DelayedDrop()
    {
        yield return new WaitForSeconds(0.05f);
        rb.isKinematic = false; // <-- this is what makes it fall!
    }

    Vector3 GetMouseWorldPlane()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return Vector3.zero;
    }
}