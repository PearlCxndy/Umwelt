using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlowdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public AudioSource audioSource;
    [Range(0.1f, 1f)]
    public float playbackSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playbackSpeed = Mathf.Max(0.1f, playbackSpeed - 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playbackSpeed = Mathf.Min(1f, playbackSpeed + 0.1f);
        }

        audioSource.pitch = playbackSpeed;
    }

}
