using UnityEngine;
using UnityEngine.Rendering;

public class SceneLightingReset : MonoBehaviour
{
    [Header("Lighting Settings")]
    public Material skyboxMaterial;
    public float ambientIntensity = 1.0f;
    public AmbientMode ambientMode = AmbientMode.Skybox;

    void Start()
    {
        // Reset lighting and skybox settings
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }

        RenderSettings.ambientMode = ambientMode;
        RenderSettings.ambientIntensity = ambientIntensity;

        // Optional: update reflection probe and global illumination
        DynamicGI.UpdateEnvironment();

        Debug.Log("Scene lighting reset complete.");
    }
}
