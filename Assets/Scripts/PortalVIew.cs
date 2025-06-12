using UnityEngine;

public class PortalView : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform animalRoot;
    public Camera portalView;
    public MeshRenderer portalMesh;
    public Shader portalShader;

    [Header("Camera Offset")]
    public float distanceInFront = 3f;
    public float orbitHeight = 1.5f;

    [Header("Sync Settings")]
    public Transform cubeReference;

    private Material portalMaterial;

    private void Start()
    {
        portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalMaterial = new Material(portalShader);
        portalMaterial.mainTexture = portalView.targetTexture;
        portalMesh.material = portalMaterial;
    }

    private void LateUpdate()
    {
        if (animalRoot == null || portalView == null || cubeReference == null)
            return;

        // Get cube Y rotation and snap it to nearest 90°
        float cubeY = cubeReference.eulerAngles.y;
        float snappedY = Mathf.Round(cubeY / 90f) * 90f;

        Vector3 offset = Vector3.zero;

        // Snap camera position based on snapped rotation
        switch (Mathf.RoundToInt(snappedY) % 360)
        {
            case 0:
                offset = new Vector3(0, orbitHeight, -distanceInFront);
                break;
            case 90:
                offset = new Vector3(distanceInFront, orbitHeight, 0);
                break;
            case 180:
            case -180:
                offset = new Vector3(0, orbitHeight, distanceInFront);
                break;
            case -90:
            case 270:
                offset = new Vector3(-distanceInFront, orbitHeight, 0);
                break;
        }

        // Apply position and rotation
        portalView.transform.position = animalRoot.position + offset;
        portalView.transform.LookAt(animalRoot.position);
    }
}
