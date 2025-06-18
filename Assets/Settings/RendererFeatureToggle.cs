using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RendererFeatureToggle : MonoBehaviour
{
    // Assign this in the inspector manually
    public ScriptableRendererFeature featureToToggle;

    public bool enableFeatureInThisScene = false;

    void Awake()
    {
        if (featureToToggle != null)
        {
            featureToToggle.SetActive(enableFeatureInThisScene);
        }
        else
        {
            Debug.LogWarning("No renderer feature assigned.");
        }
    }
}
