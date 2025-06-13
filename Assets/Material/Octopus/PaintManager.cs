using UnityEngine;

public class PaintManager : MonoBehaviour
{
    public static PaintManager Instance;

    [Header("Shader and RenderTextures")]
    public Shader drawShader;          // Drag your DrawShader here (NOT a Material!)
    public RenderTexture paintMap;
    private RenderTexture bufferRT;

    private Material drawMaterial;

    private void Awake()
    {
        Instance = this;
        drawMaterial = new Material(drawShader);

        bufferRT = new RenderTexture(paintMap.width, paintMap.height, 0, paintMap.format);
        bufferRT.enableRandomWrite = true;
        bufferRT.Create();

        Graphics.Blit(Texture2D.blackTexture, paintMap);
    }

    public void DrawAtPoint(Vector3 worldPos, GameObject paintable)
    {
        var uv = GetUVFromWorld(paintable, worldPos);
        if (uv == Vector2.zero) return;

        drawMaterial.SetVector("_UVPos", new Vector4(uv.x, uv.y, 0, 0));
        drawMaterial.SetTexture("_PaintMap", paintMap); // READ

        // DRAW into buffer
        Graphics.Blit(paintMap, bufferRT, drawMaterial);

        // COPY buffer back to main map
        Graphics.Blit(bufferRT, paintMap);
    }

    Vector2 GetUVFromWorld(GameObject obj, Vector3 point)
    {
        MeshCollider col = obj.GetComponent<MeshCollider>();
        if (!col) return Vector2.zero;

        RaycastHit hit;
        Vector3 dir = (col.bounds.center - point).normalized;
        Ray ray = new Ray(point, dir);

        if (col.Raycast(ray, out hit, 1f))
        {
            Debug.Log("UV hit at: " + hit.textureCoord);
            return hit.textureCoord;
        }

        return Vector2.zero;
    }
}