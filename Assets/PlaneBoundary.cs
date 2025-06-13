using UnityEngine;

public class PlaneBoundary : MonoBehaviour
{
    public Transform dog;
    public float pullStrength = 10f;

    void FixedUpdate()
    {
        Vector3 planePos = transform.position;
        Vector3 planeScale = transform.localScale;

        // Unity default Plane mesh is 10x10 units
        float planeWidth = 10 * planeScale.x;
        float planeLength = 10 * planeScale.z;

        float minX = planePos.x - planeWidth / 2;
        float maxX = planePos.x + planeWidth / 2;
        float minZ = planePos.z - planeLength / 2;
        float maxZ = planePos.z + planeLength / 2;

        Vector3 targetPos = dog.position;
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);

        dog.position = Vector3.MoveTowards(dog.position, targetPos, pullStrength * Time.fixedDeltaTime);
    }
}
