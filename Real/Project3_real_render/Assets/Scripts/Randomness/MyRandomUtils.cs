using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandomUtils : MonoBehaviour
{
    public GameObject[] PPrefabs;
    public GameObject[] infectedPPrefabs;



    public Transform[] playerSpawnPoints;
    public Transform[] infectedSpawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }


    public Vector3 GetSpawnPosition(bool isInfected)
    {
        Vector3 position;
        int ridx;
        if(isInfected)
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * infectedSpawnPoints.Length);
            position = infectedSpawnPoints[ridx].position;
        }
        else
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * playerSpawnPoints.Length);
            position = playerSpawnPoints[ridx].position;
        }

        return position;
    }


    public GameObject GetSpawnGameObject(bool isInfected)
    {
        GameObject PPrefab;
        int ridx;
        if(isInfected)
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * infectedPPrefabs.Length);
            PPrefab = infectedPPrefabs[ridx];
        }
        else
        {
            ridx = (int)Mathf.Floor(UnityEngine.Random.value * PPrefabs.Length);
            PPrefab = PPrefabs[ridx];

        }
        return PPrefab;
    }

}
