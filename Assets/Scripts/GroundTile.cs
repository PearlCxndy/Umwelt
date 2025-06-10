using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    GroundSpawner groundSpawner;
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        spawnObstacles();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, -1f);
    }
    void Update()
    {

    }

    public GameObject[] obstaclePrefabs;

    void spawnObstacles()


    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject chosenPrefab = obstaclePrefabs[prefabIndex];


        int angle = Random.Range(0, 2) * 180;
        Quaternion randomRotation = Quaternion.Euler(0, angle, 0);
        if (prefabIndex == 5 || prefabIndex == 6)
        {
            float randomPosX = Random.Range(-2.5f,2.5f);
            float randomPosY = Random.Range(-3f,1f);

            Vector3 offsetPosition = spawnPoint.position + new Vector3(randomPosX, randomPosY, 0f);


            Instantiate(chosenPrefab, offsetPosition, randomRotation, transform);
        }
        else
        {
            Instantiate(chosenPrefab, spawnPoint.position, randomRotation, transform);
        }


    }



}
