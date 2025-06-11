using UnityEngine;

public class PortalView : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform animalRoot;          // The model to look at
    public Camera portalView;             // The camera rendering to the portal
    public MeshRenderer portalMesh;       // The mesh showing the RenderTexture
    public Shader portalShader;           // Shader used for display

    [Header("Orbit Sync Settings")]
    public Transform cubeReference;       // Cube we orbit in sync with
    public float orbitDistance = 3f;      // Radius around animal
    public float orbitHeight = 1.5f;      // Vertical offset from animal
    public int orbitDirection = 1;        // 1 = CW, -1 = CCW
    public float orbitMultiplier = 1f;    // Speed match vs cube
    public float orbitOffset = 0f;        // Extra Y-angle offset in degrees

    private Material portalMaterial;

    private void Start()
    {
        // Setup RenderTexture
        portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // Assign shader + texture to the portal mesh
        portalMaterial = new Material(portalShader);
        portalMaterial.mainTexture = portalView.targetTexture;
        portalMesh.material = portalMaterial;
    }

    private void LateUpdate()
    {
        if (animalRoot == null || portalView == null || cubeReference == null)
            return;

        // Get the cube’s Y rotation and apply portal-specific settings
        float baseAngle = cubeReference.eulerAngles.y;
        float orbitAngle = (baseAngle + orbitOffset) * orbitDirection * orbitMultiplier;
        float radians = orbitAngle * Mathf.Deg2Rad;

        // Compute camera position in circular orbit
        Vector3 offset = new Vector3(
            Mathf.Sin(radians) * orbitDistance,
            orbitHeight,
            Mathf.Cos(radians) * orbitDistance
        );

        // Place and orient the portal camera
        portalView.transform.position = animalRoot.position + offset;
        portalView.transform.LookAt(animalRoot.position);
    }
}
