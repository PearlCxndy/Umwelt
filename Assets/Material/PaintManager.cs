using UnityEngine;

public class PaintManager : MonoBehaviour
{
    public static PaintManager Instance;

    public Shader drawShader;
    public RenderTexture paintMap;
    private Material drawMaterial;

    private void Awake()
    {
        Instance = this;
        drawMaterial = new Material(drawShader);
    }

    public void DrawAtPoint(Vector3 worldPos, GameObject paintable)
    {
        var uv = GetUVFromWorld(paintable, worldPos);
        if (uv == Vector2.zero) return;

        drawMaterial.SetVector("_UVPos", new Vector4(uv.x, uv.y, 0, 0));
        Graphics.Blit(null, paintMap, drawMaterial);
    }

    Vector2 GetUVFromWorld(GameObject obj, Vector3 point)
    {
        MeshCollider col = obj.GetComponent<MeshCollider>();
        if (!col) return Vector2.zero;

        RaycastHit hit;
        Ray ray = new Ray(point + Vector3.up * 0.1f, Vector3.down);
        if (col.Raycast(ray, out hit, 1f))
        {
            return hit.textureCoord;
        }
        return Vector2.zero;
    }
}