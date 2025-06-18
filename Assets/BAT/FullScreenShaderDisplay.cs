using UnityEngine;

[ExecuteInEditMode]
public class FullScreenShaderDisplay : MonoBehaviour
{
    public Material displayMaterial;

    private GameObject screenQuad;

    void Start()
    {
        if (displayMaterial == null)
        {
            Debug.LogWarning("No material assigned!");
            return;
        }

        // Create a quad
        screenQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        screenQuad.name = "FullScreenShaderQuad";
        screenQuad.GetComponent<MeshRenderer>().material = displayMaterial;

        // Make it a child of the camera
        screenQuad.transform.SetParent(Camera.main.transform, false);

        // Position it directly in front of the camera
        screenQuad.transform.localPosition = new Vector3(0f, 0f, 1f);
        screenQuad.transform.localRotation = Quaternion.identity;
        screenQuad.transform.localScale = new Vector3(2f, 2f, 1f); // Stretch it to fill view

        // Optional: Disable shadows
        screenQuad.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        screenQuad.GetComponent<MeshRenderer>().receiveShadows = false;
    }
}
