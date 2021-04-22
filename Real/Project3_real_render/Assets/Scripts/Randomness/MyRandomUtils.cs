using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandomUtils : MonoBehaviour
{
    //Player Objects
    public GameObject[] PPrefabs;
    public GameObject[] infectedPPrefabs;
    public Transform[] playerSpawnPoints;
    public Transform[] infectedSpawnPoints;

    //Spawn the Player in the Correct Spot
    public Vector3 GetSpawnPosition(bool isInfected)
    {
        Vector3 position;
        int ridx;
        //Spawning Infected
        if(isInfected)
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * infectedSpawnPoints.Length);
            position = infectedSpawnPoints[ridx].position;
        }
        //Spawning Normal
        else
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * playerSpawnPoints.Length);
            position = playerSpawnPoints[ridx].position;
        }
        return position;
    }

    //Get Spawned Player
    public GameObject GetSpawnGameObject(bool isInfected)
    {
        GameObject PPrefab;
        int ridx;
        //Infected Player
        if(isInfected)
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * infectedPPrefabs.Length);
            PPrefab = infectedPPrefabs[ridx];
        }
        //Normal Player
        else
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * PPrefabs.Length);
            PPrefab = PPrefabs[ridx];

        }
        return PPrefab;
    }

}
