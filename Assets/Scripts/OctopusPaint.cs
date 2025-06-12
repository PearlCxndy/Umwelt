using UnityEngine;

public class OctopusPaint : MonoBehaviour
{
    public Material targetMaterial;  // Assign your TouchPaintMat in inspector
    public float radius = 0.2f;
    public Color paintColor = Color.red;

    private void OnTriggerStay(Collider other)
    {
        if (targetMaterial == null) return;

        // Send the contact position and color to the shader
        Vector3 contact = transform.position;

        targetMaterial.SetVector("_TouchPosition", contact);
        targetMaterial.SetFloat("_Radius", radius);
        targetMaterial.SetColor("_PaintColor", paintColor);
    }
}