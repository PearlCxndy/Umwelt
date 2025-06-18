using UnityEngine;

public class PortalView : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform animalRoot;          
    public Camera portalView;             
    public MeshRenderer portalMesh;       
    public Shader portalShader;           

    [Header("Animal Root Offset")]
    public float animalYOffset = 0f;      // <-- New field to adjust Y position of animal root

    [Header("Camera Offset")]
    public float cameraHeight = 1.5f;     

    [Header("Orbit Sync Settings")]
    public Transform cubeReference;       
    public float orbitDistance = 3f;      
    public int orbitDirection = 1;        
    public float orbitMultiplier = 1f;    
    public float orbitOffset = 0f;        

    private Material portalMaterial;

    private void Start()
    {
        portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalMaterial = new Material(portalShader);
        portalMaterial.mainTexture = portalView.targetTexture;
        portalMesh.material = portalMaterial;

        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        if (animalRoot == null || portalView == null || cubeReference == null)
            return;

        UpdateCameraPosition();
    }

    private Vector3 GetAnimalCenter()
    {
        Renderer renderer = animalRoot.GetComponentInChildren<Renderer>();
        if (renderer != null)
            return renderer.bounds.center + new Vector3(0, animalYOffset, 0);
        else
            return animalRoot.position + new Vector3(0, animalYOffset, 0);
    }

    private void UpdateCameraPosition()
    {
        float baseAngle = cubeReference.eulerAngles.y;
        float orbitAngle = (baseAngle + orbitOffset) * orbitDirection * orbitMultiplier;
        float radians = orbitAngle * Mathf.Deg2Rad;

        // Calculate flat horizontal orbit
        Vector3 offset = new Vector3(
            Mathf.Sin(radians) * orbitDistance,
            0f,
            Mathf.Cos(radians) * orbitDistance
        );

        // Apply vertical camera height
        Vector3 cameraPos = animalRoot.position + offset;
        cameraPos.y += cameraHeight;

        Vector3 lookTarget = GetAnimalCenter();
        portalView.transform.position = cameraPos;
        portalView.transform.LookAt(lookTarget);
    }
}
