using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tragetPos = player.position + offset;
        tragetPos.x = 0;
        transform.position = tragetPos;
    }
}
