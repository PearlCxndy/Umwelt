using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
//reference: https://www.youtube.com/watch?v=wtXirrO-iNA and https://discussions.unity.com/t/getoutputdata-and-getspectrumdata-what-represent-the-values-returned/27063 

public class SoundVisual : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Audio Extract")]
    private const int SAMPLE_SIZE = 1024;
    public float rmsValue; // avg power output of the sound
    public float dbValue; // sound value during that frame
    public float pitchValue; //pitch duh

    [Header("Audio Reactivity Visuals")]
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


    [Header("Visualizer Layout")]
    public float radius = 10.0f; // default radius, scaled dynamically
    public float width = 1.0f;
    public float height = 1.0f;
    public float depth = 1.0f;
    private Vector3[] baseScales;

    [Header("Visualizer Appearance")]
    public Material cubeMaterial;



    //UI Canvas
    [Header("UI Canvas Setup")]
    public Transform canvasParent;  


    void Start()
    {
        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;

        // SpawnLine();
        SpawnCircle();
       
    }

    private void SpawnCircle()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        baseScales = new Vector3[amnVisual];

        Vector3 center = Vector3.zero;

        // // Dynamically adjust radius based on canvas scale
        // float canvasScaleFactor = canvasParent != null ? canvasParent.localScale.x : 1.0f;
        // float adjustedRadius = radius * canvasScaleFactor;

        float adjustedRadius = radius; // no scaling


        for (int i = 0; i < amnVisual; i++)
        {
            float ang = (i * 1.0f / amnVisual) * Mathf.PI * 2;

            float x = center.x + Mathf.Cos(ang) * adjustedRadius;
            float y = center.y + Mathf.Sin(ang) * adjustedRadius;

            Vector3 pos = new Vector3(x, y, 0);

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.SetParent(canvasParent, false);

            Renderer renderer = go.GetComponent<Renderer>();
            if (cubeMaterial != null)
            {
                renderer.material = cubeMaterial;
            }

            go.transform.localScale = new Vector3(width, height, depth);
            baseScales[i] = go.transform.localScale;

            go.transform.localPosition = pos;
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, pos);

            
            visualList[i] = go.transform;
        }
    }



    //create line of cubes
    private void SpawnLine()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        baseScales = new Vector3[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.SetParent(canvasParent, false);
            
            go.transform.localScale = new Vector3(width, height, depth);
            baseScales[i] = go.transform.localScale;

            float spacing = 20f; // experiment with this based on canvas size
            go.transform.localPosition = new Vector3(i * spacing, 0, 0);

            visualList[i] = go.transform;
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
    int averageSize = (int)(SAMPLE_SIZE * keepPercentage) / amnVisual;

    while (visualIndex < amnVisual)
    {
        int j = 0;
        float sum = 0;

        while (j < averageSize && spectrumIndex < spectrum.Length)
        {
            sum += spectrum[spectrumIndex];
            spectrumIndex++;
            j++;
        }

        float scaleY = sum / averageSize * visualModifier;
        visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

        if (visualScale[visualIndex] < scaleY)
        {
            visualScale[visualIndex] = scaleY;
        }

        if (visualScale[visualIndex] > maxVisualScale)
        {
            visualScale[visualIndex] = maxVisualScale;
        }

        // Use base scale and only modify Y
        Vector3 baseScale = baseScales[visualIndex];
        Vector3 newScale = new Vector3(
            baseScale.x,
            baseScale.y + visualScale[visualIndex],
            baseScale.z
        );

        visualList[visualIndex].localScale = newScale;
        visualIndex++;
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

        for (i = 0; i < SAMPLE_SIZE; i++)
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
