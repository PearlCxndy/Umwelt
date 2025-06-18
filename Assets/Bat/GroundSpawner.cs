//Used these video Series to create an Infinite runner game: https://www.youtube.com/watch?v=XUVtQwKbZ-c&list=PLvcJYjdXa962PHXFjQ5ugP59Ayia-rxM3&index=4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;


    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
    }
    private void Start()
    {
        for (int i = 0; i < 100; i++) {
            SpawnTile();
       }
    }
}
