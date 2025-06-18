//Used this video and the resources listed, to create a shader: https://www.youtube.com/watch?v=QAsEkajvwm8
//Screen position to world postion: https://discussions.unity.com/t/computing-world-position-from-depth/761733
//Used chatGPT to debug: https://chatgpt.com/share/684b5e3d-bf54-800b-9f7a-cc4d142da50a
//https://chatgpt.com/share/684b5e6b-f064-800b-bd4d-b2498fcee143

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