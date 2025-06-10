using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScanner : MonoBehaviour
{
    public Material screenMat;
    public Transform screenObject;

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 100 == 0)
        {
            Debug.Log("Click detected! Starting scan...");
            StartCoroutine(SceneScanning());
        }
        if (Input.anyKeyDown)
            {
                Debug.Log("Click detected! Starting scan...");
                StartCoroutine(SceneScanning());

            }
    }

    public IEnumerator SceneScanning()
    {
        float Timer = 0;
        float scanRange = 0;
        float opacity = 1;
        screenMat.SetVector("_Position", screenObject.position);

        while (true)
        {
            Timer += Time.deltaTime;
            if (Timer <= 1)
            {
                scanRange = Mathf.Lerp(0, 100, Timer);
                opacity = Mathf.Lerp(100, 10, Timer);
                screenMat.SetFloat("_Range", scanRange);
                screenMat.SetFloat("_Opacity", opacity);
            }
            else
                yield break;
            yield return null;
        } 
       
    }
}
