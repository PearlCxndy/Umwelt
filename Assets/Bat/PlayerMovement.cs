//Used these video Series to create an Infinite runner game: https://www.youtube.com/watch?v=XUVtQwKbZ-c&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=4

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;
    public float speed = 20;
    public Rigidbody rb;

    float horizontalInput;
    float verticalInput;

    public float horizontalMultiplier = 1;
    public float verticalMultiplier = 1;

    //Limiting the distance that the player can move
    public float horizontalLimit = 1;
    public float verticalLimit = 3;
    // Tilting the player as it moves horizontally (for a more organic flying effect). 
    // I asked chatGpt how to do it: https://chatgpt.com/share/684b504c-29f4-800b-9fe9-a21f72aa06be
    public float tiltAngle=30;
    public float tiltSpeed=5;
    private void FixedUpdate()
    {
        if (!alive) return;

        //I used chatGPT to limit the horizontal and vertical movement distances: https://chatgpt.com/share/684b504c-29f4-800b-9fe9-a21f72aa06be
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * horizontalMultiplier * Time.fixedDeltaTime;
        Vector3 verticalMove = transform.up * verticalInput * speed * verticalMultiplier;

        Vector3 newPos = rb.position + forwardMove + horizontalMove + verticalMove;

        newPos.x = Mathf.Clamp(newPos.x, -horizontalLimit, horizontalLimit);
        newPos.y = Mathf.Clamp(newPos.y, 1, verticalLimit);
        rb.MovePosition(newPos);
        //Tilt
        float tiltZ = -horizontalInput * tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, tiltZ);

        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * tiltSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    public void Die()
    {
        alive = false;
        //Restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
