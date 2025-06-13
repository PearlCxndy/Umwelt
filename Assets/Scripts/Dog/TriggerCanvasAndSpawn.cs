using UnityEngine;

public class TriggerCanvasAndSpawn : MonoBehaviour
{
    [Header("References")]
    public Canvas canvasImage;                  
    public GameObject objectToAppear;           
    public GameObject particleTrailPrefab;      
    public Transform trailStartPoint;           
    public Transform trailEndPoint;             
    public ParticleSystem fogParticleSystem;    
    public Transform dog;                       // Dog reference for following

    private bool playerInTrigger = false;
    private GameObject player;

    private bool particleSpawned = false;
    private bool fogActivated = false;

    void Start()
    {
        if (canvasImage != null)
            canvasImage.enabled = false;

        if (objectToAppear != null)
            objectToAppear.SetActive(false);

        if (fogParticleSystem != null)
            fogParticleSystem.Stop();
    }

    void Update()
    {
        // Move fog to follow dog if activated
        if (fogActivated && fogParticleSystem != null && dog != null)
        {
            fogParticleSystem.transform.position = dog.position;
        }

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

                // Activate fog once when pressing E
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
