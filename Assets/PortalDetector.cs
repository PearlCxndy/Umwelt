using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PortalDetector : MonoBehaviour
{
    [System.Serializable]
    public class Portal
    {
        public string sceneName;
        public float targetYAngle; // Exact Y rotation to match
    }

    [Header("Portal Configuration")]
    public CubeRotator cubeRotator;
    public List<Portal> portals = new List<Portal>();

    [Header("Detection Settings")]
    [Range(1f, 45f)]
    public float angleTolerance = 20f;

    [Header("Debug")]
    public bool showDebug = true;

    private void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Q))
        {
            TryEnterPortal();
        }
    }

    private void TryEnterPortal()
    {
        if (cubeRotator == null)
        {
            Debug.LogWarning("CubeRotator reference is missing.");
            return;
        }

        float currentY = NormalizeAngle(cubeRotator.GetCurrentYRotation());

        if (showDebug)
            Debug.Log($"🎯 Cube Y Rotation: {currentY}");

        foreach (var portal in portals)
        {
            float target = NormalizeAngle(portal.targetYAngle);
            float delta = Mathf.Abs(Mathf.DeltaAngle(currentY, target));

            if (showDebug)
                Debug.Log($"Checking portal: {portal.sceneName} | Target: {target}° | Δ: {delta}°");

            if (delta <= angleTolerance)
            {
                Debug.Log($"✅ Entering portal: {portal.sceneName}");
                SceneManager.LoadScene(portal.sceneName);
                return;
            }
        }

        Debug.Log("❌ No portal matched.");
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return angle < 0 ? angle + 360f : angle;
    }
}
