using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//adapted from tutorial: https://youtu.be/t3kr_oBuGfo

public class Spectrum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[256]; //where spectrum data will be saved

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < spectrum.Length; i++)
        {

            float tmp = spectrum[i] * 100;

            if (tmp >= 3f)
            {
                gameObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            }
            Debug.Log(spectrum[i]);
        }

    }
}
