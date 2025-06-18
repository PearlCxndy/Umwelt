using System.Collections;
using UnityEngine;

public class ScreenScannerSample : MonoBehaviour
{
    public Material screenMat;
    public Transform screenObject;


    private float scanTimer = 0;

    void Update()
    {


        scanTimer += Time.deltaTime;

        if (scanTimer >= 2)
        {
            Debug.Log("Click detected! Starting scan...");
            StartCoroutine(SceneScanningSample());
            scanTimer = 0;
        }
    }

    public IEnumerator SceneScanningSample()
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
                scanRange = Mathf.Lerp(0, 50, Timer);
                opacity = Mathf.Lerp(50, 10, Timer);
                screenMat.SetFloat("_Range", scanRange);
                screenMat.SetFloat("_Opacity", opacity);
            }
            else
                yield break;
            yield return null;
        }
    }
}
