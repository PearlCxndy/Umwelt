using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshCollider))]
public class PaintableObject : MonoBehaviour
{
    public int textureSize = 1024;
    public Material drawMaterial;           // Shared Draw Material
    public Material revealMaterialTemplate; // Shared Reveal Material (Shader Graph)
    
    private RenderTexture paintMap;
    private RenderTexture processedMap;
    private Material revealMaterialInstance;
    private MaterialPropertyBlock propBlock;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        // Make per-object texture
        paintMap = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGBFloat);
        paintMap.enableRandomWrite = true;
        paintMap.Create();

        processedMap = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGBFloat);
        processedMap.enableRandomWrite = true;
        processedMap.Create();

        // Clone the reveal material per object so each can use its own texture
        revealMaterialInstance = new Material(revealMaterialTemplate);

        // Assign the processed texture to the cloned material
        revealMaterialInstance.SetTexture("_ProcessedMap", processedMap);

        // Apply to this object's renderer
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = revealMaterialInstance;

        // Optional: store the block if you're using property blocks (not necessary here)
        propBlock = new MaterialPropertyBlock();
    }

    public void DrawAtUV(Vector2 uv)
    {
        drawMaterial.SetVector("_UVPos", new Vector4(uv.x, uv.y, 0, 0));
        drawMaterial.SetTexture("_PaintMap", paintMap);

        // Paint into buffer, then copy to main paintMap
        Graphics.Blit(paintMap, processedMap, drawMaterial);
    }

    public RenderTexture GetPaintMap() => paintMap;
}