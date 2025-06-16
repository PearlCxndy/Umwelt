using UnityEngine;

public class TentacleTargetExplorer : MonoBehaviour
{
    public Transform octopusBody;
    public Vector3 defaultLocalOffset = new Vector3(1f, 0f, 0f); // default position relative to octopus
    public float detectionRadius = 5f;
    public float returnSpeed = 2f;
    public float wanderRadius = 1f;
    public float wanderSpeed = 1f;

    private Transform currentSurface;
    private Vector3 surfaceOffset;
    private float wanderTimer;
    private Vector3 targetWanderPos;

    void Update()
    {
        // 1. Try to find a nearby surface tagged "Paintable"
        Collider[] hits = Physics.OverlapSphere(octopusBody.position, detectionRadius);
        currentSurface = null;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Paintable"))
            {
                currentSurface = hit.transform;
                break;
            }
        }

        if (currentSurface)
        {
            WanderOnSurface(currentSurface);
        }
        else
        {
            ReturnToBody();
        }
    }

    void ReturnToBody()
    {
        Vector3 worldTargetPos = octopusBody.TransformPoint(defaultLocalOffset);
        transform.position = Vector3.Lerp(transform.position, worldTargetPos, Time.deltaTime * returnSpeed);
    }

    void WanderOnSurface(Transform surface)
    {
        wanderTimer += Time.deltaTime * wanderSpeed;

        if (wanderTimer > 1f)
        {
            // Random offset near the surface
            Vector3 randomDir = Random.onUnitSphere;
            Vector3 randomPoint = surface.position + randomDir * wanderRadius;

            RaycastHit hit;
            if (Physics.Raycast(randomPoint + Vector3.up * 3f, Vector3.down, out hit, 10f))
            {
                if (hit.transform == surface)
                {
                    targetWanderPos = hit.point;
                }
            }

            wanderTimer = 0.1f;
        }

        transform.position = Vector3.Lerp(transform.position, targetWanderPos, Time.deltaTime * wanderSpeed);
    }
}