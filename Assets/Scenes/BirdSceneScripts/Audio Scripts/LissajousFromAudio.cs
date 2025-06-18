using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LissajousFromFrequencyBands : MonoBehaviour
{
    public AudioSource audioSource;
    public FFTWindow fftWindow = FFTWindow.Blackman;
    public int spectrumSize = 1024;
    public int resolution = 512;
    public float scale = 5f;

    private LineRenderer lineRenderer;
    private float[] spectrum;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = resolution;
        spectrum = new float[spectrumSize];
    }

    void Update()
    {
        if (!audioSource || !audioSource.isPlaying)
            return;

        // Get real-time FFT data (mono or combined L+R)
        audioSource.GetSpectrumData(spectrum, 0, fftWindow);

        // Find the two dominant frequency bins
        int bin1 = 0, bin2 = 1;
        float mag1 = 0f, mag2 = 0f;

        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > mag1)
            {
                mag2 = mag1;
                bin2 = bin1;

                mag1 = spectrum[i];
                bin1 = i;
            }
            else if (spectrum[i] > mag2)
            {
                mag2 = spectrum[i];
                bin2 = i;
            }
        }

        // Convert bin indices to visual frequencies
        // SampleRate / 2 = Nyquist freq = max FFT freq range
        float freq1 = Mathf.Lerp(1f, 30f, bin1 / (float)spectrumSize);
        float freq2 = Mathf.Lerp(1f, 30f, bin2 / (float)spectrumSize);

        // Create the Lissajous figure
        float step = Mathf.PI * 2f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            float t = i * step;
            float x = Mathf.Sin(freq1 * t);
            float y = Mathf.Sin(freq2 * t + Mathf.PI / 2); // 90° phase offset
            lineRenderer.SetPosition(i, new Vector3(x, y, 0f) * scale);
        }
    }
}


