//TUTORIAL REFERENCE: https://www.youtube.com/watch?v=6-pJu0GwK5k
//This code makes the moth/ bug fly in a more organic way.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingReward : MonoBehaviour {

    public float speed=1f;
    public float horizontalAmplitude = 1f;
    public float verticalAmplitude=1f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition += transform.position;
    }

    void FixedUpdate()
    {
        //I used chatGPT here to fix the issue of the object moving in only one 
        // direction rather than doing a loop: https://chatgpt.com/share/684a364f-7760-800b-b2f5-5189cffb3cc8

        float t = Time.time * speed;
        float newX= startPosition.x += Mathf.Sin(t) * horizontalAmplitude;
        float newY= startPosition.y += Mathf.Sin(t*2f + Mathf.PI / 2)*verticalAmplitude;
        transform.position = new Vector3(newX, newY, startPosition.z);
    }
}
