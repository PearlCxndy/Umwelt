//Used these video series: https://www.youtube.com/watch?v=XUVtQwKbZ-c&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=5

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;
       


    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
    private void OnCollissionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //Kill Player
            playerMovement.Die();
        }
 }
    void Update()
    {
        
    }
}
