using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    //Set teleport target and location
    public Transform teleportTarget;
    public GameObject player;

    //Teleport the player when they collide
    void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportTarget.transform.position;
    }
}
