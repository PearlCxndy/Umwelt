using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class PitchECGGraph : MonoBehaviour
{
    public AudioSource audioSource;
    public FFTWindow fftWindow = FFTWindow.BlackmanHarris;
    public int spectrumSize = 1024;
    public float sampleRate = 44100f;

    public float updateRate = 0.05f;  // how often to sample (sec)
    public float visibleWidth = 10f;  // how wide the graph should appear (units)
    public float pitchScale = 0.05f;  // vertical scale (Hz → Y)
    public int maxPoints = 200;       // points visible on screen

    private LineRenderer lineRenderer;
    private float[] spectrum;
    private List<float> pitchBuffer = new List<float>();
    private float timer = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        spectrum = new float[spectrumSize];
    }

    void Update()
    {
        if (!audioSource || !audioSource.isPlaying) return;

        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;

            // Get FFT and extract dominant frequency
            audioSource.GetSpectrumData(spectrum, 0, fftWindow);

            int peakIndex = 0;
            float peakMag = 0f;

            for (int i = 1; i < spectrum.Length; i++)
            {
                if (spectrum[i] > peakMag)
                {
                    peakMag = spectrum[i];
                    peakIndex = i;
                }
            }

            float freq = peakIndex * (sampleRate / 2f) / spectrumSize;
            pitchBuffer.Add(freq);

            // Limit number of stored points
            if (pitchBuffer.Count > maxPoints)
                pitchBuffer.RemoveAt(0);

            // Rebuild line points, mapped to fixed X width
            Vector3[] linePoints = new Vector3[pitchBuffer.Count];
            float dx = visibleWidth / maxPoints;

            for (int i = 0; i < pitchBuffer.Count; i++)
            {
                float x = i * dx;
                float y = pitchBuffer[i] * pitchScale;
                linePoints[i] = new Vector3(x, y, 0f);
            }

            lineRenderer.positionCount = linePoints.Length;
            lineRenderer.SetPositions(linePoints);
        }
    }
}
