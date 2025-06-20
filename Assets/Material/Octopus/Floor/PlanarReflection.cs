using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarReflection : MonoBehaviour
{
    private Vector2 Resolution;

    [SerializeField] private Camera ReflectionCamera;
    [SerializeField] private RenderTexture ReflectionRenderTexture;
    [SerializeField] private int ReflectionResloution;
    [SerializeField] private Camera SourceCamera;
    [SerializeField] private string SourceCameraTag = "ReflectionSource";

    private void Start()
    {
        GameObject cameraObj = GameObject.FindGameObjectWithTag(SourceCameraTag);
        if (cameraObj != null)
            SourceCamera = cameraObj.GetComponent<Camera>();
    }


    //     private void LateUpdate()
    //     {
    //         ReflectionCamera.fieldOfView = Camera.main.fieldOfView;
    //         ReflectionCamera.transform.position = new Vector3(Camera.main.transform.position.x, -Camera.main.transform.position.y + transform.position.y, Camera.main.transform.position.z);
    //         ReflectionCamera.transform.rotation = Quaternion.Euler(-Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0f);

    //         Resolution = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

    //         ReflectionRenderTexture.Release();
    //         ReflectionRenderTexture.width = Mathf.RoundToInt(Resolution.x) * ReflectionResloution / Mathf.RoundToInt(Resolution.y);
    //         ReflectionRenderTexture.height = ReflectionResloution;
    //     }
    // }

    private void LateUpdate()
    {
        if (SourceCamera == null || ReflectionCamera == null || ReflectionRenderTexture == null)
            return;

        // Copy field of view
        ReflectionCamera.fieldOfView = SourceCamera.fieldOfView;

        // Mirror position across the water plane (assuming this object's Y is the reflection plane)
        Vector3 camPosition = SourceCamera.transform.position;
        camPosition.y = -camPosition.y + transform.position.y * 2f;
        ReflectionCamera.transform.position = camPosition;

        // Mirror rotation on X axis
        Vector3 camEuler = SourceCamera.transform.eulerAngles;
        camEuler.x = -camEuler.x;
        ReflectionCamera.transform.rotation = Quaternion.Euler(camEuler);

        // Match resolution
        Resolution = new Vector2(SourceCamera.pixelWidth, SourceCamera.pixelHeight);

        ReflectionRenderTexture.Release();
        ReflectionRenderTexture.width = Mathf.RoundToInt(Resolution.x) * ReflectionResloution / Mathf.RoundToInt(Resolution.y);
        ReflectionRenderTexture.height = ReflectionResloution;

        ReflectionCamera.targetTexture = ReflectionRenderTexture;
    }
}
