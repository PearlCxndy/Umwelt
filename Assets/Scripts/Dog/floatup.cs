using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class floatup : MonoBehaviour
{
    public float targetY = 5f;  // Set this in the inspector
    public float speed = 2f;    // Floating speed

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.y < targetY)
        {
            float newY = Mathf.MoveTowards(currentPosition.y, targetY, speed * Time.deltaTime);
            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
