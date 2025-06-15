using UnityEngine;

public class TriggerCanvasAndSpawn : MonoBehaviour
{
    [Header("References")]
    public Canvas canvasImage;
    public GameObject sphereObject; 
    public GameObject objectToAppear;
    public GameObject particleTrailPrefab;
    public Transform trailStartPoint;
    public Transform trailEndPoint;
    public ParticleSystem fogParticleSystem;
    public Transform dog;

    private bool playerInTrigger = false;
    private GameObject player;
    private bool particleSpawned = false;
    private bool fogActivated = false;

    void Start()
    {
        if (canvasImage != null)
            canvasImage.enabled = false;

        if (sphereObject != null)
            sphereObject.SetActive(false);  // sphere starts invisible

        if (fogParticleSystem != null)
            fogParticleSystem.Stop();

        if (objectToAppear != null)
        objectToAppear.SetActive(false);

    }

    void Update()
    {
        if (fogActivated && fogParticleSystem != null && dog != null)
        {
            fogParticleSystem.transform.position = dog.position;
        }

        if (playerInTrigger && player != null && canvasImage != null && canvasImage.enabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Show sphere
                if (sphereObject != null && !sphereObject.activeSelf)
                {
                    sphereObject.SetActive(true);
                    Debug.Log("Sphere appeared.");
                }

                // Spawn particle trail
                if (!particleSpawned && particleTrailPrefab != null)
                {
                    if (trailStartPoint == null || trailEndPoint == null)
                    {
                        Debug.LogError("Start or End point not assigned!");
                        return;
                    }

                    GameObject particle = Instantiate(particleTrailPrefab);
                    CurvedTrailMover mover = particle.GetComponent<CurvedTrailMover>();

                    if (objectToAppear != null && !objectToAppear.activeSelf)
                    {
                        objectToAppear.SetActive(true);
                        Debug.Log("Other object appeared.");
                    }

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

                // Activate fog
                if (!fogActivated && fogParticleSystem != null)
                {
                    fogParticleSystem.Play();
                    fogActivated = true;
                    Debug.Log("Fog activated and following dog.");
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
