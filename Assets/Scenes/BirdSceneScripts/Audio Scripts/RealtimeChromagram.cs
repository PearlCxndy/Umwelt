using UnityEngine;
using UnityEngine.UI;

public class RealtimeChromagram : MonoBehaviour
{
    [Header("References")]
    public RawImage rawImage;         // Assign the UI RawImage
    public AudioSource audioSource;   // The AudioSource playing your sound

    [Header("Visualization Settings")]
    public int textureWidth = 512;    // Time (scrolling)
    public int chromaHeight = 12;     // 12 pitch classes
    public float updateInterval = 0.05f; // seconds between updates

    [Header("FFT Settings")]
    public int fftSize = 1024;        // Must be a power of 2

    private Texture2D texture;
    private float[] spectrum;
    private float timer;
    private int currentColumn;

    void Start()
    {
        texture = new Texture2D(textureWidth, chromaHeight, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;

        // Initialize texture to black
        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < chromaHeight; y++)
                texture.SetPixel(x, y, Color.black);
        }
        texture.Apply();

        rawImage.texture = texture;
        spectrum = new float[fftSize];
        currentColumn = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            DrawNextColumn();
        }
    }

    void DrawNextColumn()
    {
        // Get spectrum
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float[] chromaBins = new float[chromaHeight];

        // Map FFT bins to 12 pitch classes
        for (int i = 1; i < fftSize; i++) // skip DC bin
        {
            float freq = i * AudioSettings.outputSampleRate / 2f / fftSize;
            int pitchClass = FrequencyToPitchClass(freq);
            if (pitchClass >= 0 && pitchClass < chromaHeight)
                chromaBins[pitchClass] += spectrum[i];
        }

        // Normalize and draw to texture
        for (int y = 0; y < chromaHeight; y++)
        {
            float value = Mathf.Clamp01(chromaBins[y] * 50); // scale for visibility
            Color c = Color.HSVToRGB((float)y / 12f, 1f, value);
            texture.SetPixel(currentColumn, y, c);
        }

        texture.Apply();

        // Advance column (wrap around for continuous scrolling)
        currentColumn = (currentColumn + 1) % textureWidth;
    }

    // Convert frequency (Hz) to pitch class (0–11, where 0 = C)
    int FrequencyToPitchClass(float freq)
    {
        if (freq < 20f || freq > 5000f) return -1; // outside voice range
        float midi = 69 + 12 * Mathf.Log(freq / 440f, 2);
        return Mathf.RoundToInt(midi) % 12;
    }
}


