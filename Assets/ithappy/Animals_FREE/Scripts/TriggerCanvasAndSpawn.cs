using UnityEngine;

public class TriggerCanvasAndSpawn : MonoBehaviour
{
    [Header("References")]
    public Canvas canvasImage;                  // The interaction UI
    public GameObject objectToAppear;           // The object (e.g. picture) that becomes visible on press
    public GameObject particleTrailPrefab;      // Prefab with CurvedTrailMover script
    public Transform trailStartPoint;           // Typically the Tree or interaction origin
    public Transform trailEndPoint;             // The draggable destination target

    private bool playerInTrigger = false;
    private GameObject player;

    private bool particleSpawned = false;

    void Start()
    {
        if (canvasImage != null)
            canvasImage.enabled = false;

        if (objectToAppear != null)
            objectToAppear.SetActive(false);
    }

    void Update()
    {
        if (playerInTrigger && player != null && canvasImage != null && canvasImage.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Show object
                if (objectToAppear != null)
                {
                    objectToAppear.SetActive(true);
                    Debug.Log("Object appeared after pressing E.");
                }

                // Spawn particle trail only once
                if (!particleSpawned && particleTrailPrefab != null)
                {
                    if (trailStartPoint == null || trailEndPoint == null)
                    {
                        Debug.LogError("Start or End point not assigned!");
                        return;
                    }

                    GameObject particle = Instantiate(particleTrailPrefab);
                    CurvedTrailMover mover = particle.GetComponent<CurvedTrailMover>();

                    if (mover != null)
                    {
                        mover.Initialize(trailStartPoint, trailEndPoint);
                        particleSpawned = true;
                        Debug.Log("Trail spawned and initialized.");
                    }
                    else
                    {
                        Debug.LogError("CurvedTrailMover script missing on prefab.");
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            player = other.gameObject;

            if (canvasImage != null)
            {
                canvasImage.enabled = true;
                Debug.Log("Canvas Enabled");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            player = null;

            if (canvasImage != null)
            {
                canvasImage.enabled = false;
                Debug.Log("Canvas Disabled");
            }
        }
    }
}
