using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class InteractionShader : MonoBehaviour
{
     public Transform target; // assign dog transform
    public Vector3 offset;   // optional offset if needed

    void Update()
    {
        transform.position = target.position + offset;
    }
}
