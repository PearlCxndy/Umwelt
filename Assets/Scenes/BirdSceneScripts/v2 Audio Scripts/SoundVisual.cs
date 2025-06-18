using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//reference: https://www.youtube.com/watch?v=wtXirrO-iNA and https://discussions.unity.com/t/getoutputdata-and-getspectrumdata-what-represent-the-values-returned/27063 

public class SoundVisual : MonoBehaviour
{
    // Start is called before the first frame update

    private const int SAMPLE_SIZE = 1024;
    public float rmsValue; // avg power output of the sound
    public float dbValue; // sound value during that frame
    public float pitchValue; //pitch duh

    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10.0f;
    public float keepPercentage = 0.5f;



    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;
    public int amnVisual = 64;

    void Start()
    {
        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        SpawnLine();
    }


    //create line of cubes
    private void SpawnLine()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            visualList[i] = go.transform;
            visualList[i].position = Vector3.right * i; //adjust position here with offset maybe?

        }
    }
    // Update is called once per frame
    void Update()
    {
        AnalyzeSound(); //analyze every frame
        UpdateVisual();
    }

    //make cubes move
    private void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)(SAMPLE_SIZE * keepPercentage) / amnVisual; //for every cube we get a certain amnt of the sample

        while (visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;

            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier; //vismod is public and can be adjusted
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

            if (visualScale[visualIndex] < scaleY)
            {
                visualScale[visualIndex] = scaleY;
            }

            if (visualScale[visualIndex] > maxVisualScale)
            {
                visualScale[visualIndex] = maxVisualScale; //clamps cubes upwards
            }

            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++; //afect next object
        }
    }

    private void AnalyzeSound()
    {
        source.GetOutputData(samples, 0); //takes array of samples and channel

        //get rms value
        int i = 0;
        float sum = 0;

        for (; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }

        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);

        //get db value
        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);


        // get sound spectrum

        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);


        //find pitch value

        float maxV = 0;
        var maxN = 0;

        for (i = 0; i > SAMPLE_SIZE; i++)
        {
            if (!(spectrum[i] > maxV) || !(spectrum[i] > 0.0f))
                continue;

            maxV = spectrum[i];
            maxN = i;
        }

        float freqN = maxN;
        if (maxN > 0 && maxN < SAMPLE_SIZE - 1)
        {
            var dL = spectrum[maxN - 1] / spectrum[maxN];
            var dR = spectrum[maxN + 1] / spectrum[maxN];

            freqN += 0.5f * (dR * dR - dL * dL);

        }

        pitchValue = freqN * (sampleRate / 2) / SAMPLE_SIZE;

    }
}
