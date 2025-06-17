using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProximityLissajous : MonoBehaviour
{
    public Transform player;
    public Transform targetAudioSourceObj;
    public float activationDistance = 10f;

    public int resolution = 512;
    public float scale = 5f;

    private LineRenderer lineRenderer;
    private AudioSource audioSource;
    private float[] leftSamples;
    private float[] rightSamples;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution;

        audioSource = targetAudioSourceObj.GetComponent<AudioSource>();
        leftSamples = new float[resolution];
        rightSamples = new float[resolution];
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, targetAudioSourceObj.position);

        if (dist < activationDistance && audioSource.isPlaying)
        {
            // Get audio samples
            audioSource.GetOutputData(leftSamples, 0); // Left
            audioSource.GetOutputData(rightSamples, 1); // Right

            // Draw Lissajous-like figure
            for (int i = 0; i < resolution; i++)
            {
                Vector3 pos = new Vector3(
                    leftSamples[i] * scale,
                    rightSamples[i] * scale,
                    0f
                );
                lineRenderer.SetPosition(i, pos);
            }

            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}

