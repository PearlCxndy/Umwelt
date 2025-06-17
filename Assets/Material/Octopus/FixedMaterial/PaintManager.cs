using UnityEngine;

[RequireComponent(typeof(MeshCollider), typeof(Renderer))]
public class PaintManager : MonoBehaviour
{
    [Header("Shader and RenderTextures")]
    public Shader drawShader;
    public RenderTexture paintMap;          // unique per object

    RenderTexture bufferRT;
    Material       drawMaterial;
    MeshCollider   col;

    void Awake()
    {
        drawMaterial = new Material(drawShader);
        col          = GetComponent<MeshCollider>();

        bufferRT = new RenderTexture(paintMap.width, paintMap.height, 0, paintMap.format);
        bufferRT.enableRandomWrite = true;
        bufferRT.Create();

        // Clear once
        Graphics.Blit(Texture2D.blackTexture, paintMap);

        // Push this RT into the object’s material so Shader Graph can read it
        var block = new MaterialPropertyBlock();
        GetComponent<Renderer>().GetPropertyBlock(block);
        block.SetTexture("_PaintMap", paintMap);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    // Called by the arm
    public void DrawAtPoint(Vector3 worldPos, GameObject paintable)
    {
        Vector2 uv = GetUVFromWorld(worldPos);
        if (uv == Vector2.zero) return;

        drawMaterial.SetVector("_UVPos", new Vector4(uv.x, uv.y, 0, 0));
        drawMaterial.SetTexture("_PaintMap", paintMap);   // read

        Graphics.Blit(paintMap, bufferRT, drawMaterial);  // draw into buffer
        Graphics.Blit(bufferRT, paintMap);                // copy back
    }

    Vector2 GetUVFromWorld(Vector3 point)
    {
        Vector3 dir = (col.bounds.center - point).normalized;
        Ray ray     = new Ray(point - dir * 0.05f, dir);

        if (col.Raycast(ray, out RaycastHit hit, 1f))
            return hit.textureCoord;

        return Vector2.zero;
    }
}