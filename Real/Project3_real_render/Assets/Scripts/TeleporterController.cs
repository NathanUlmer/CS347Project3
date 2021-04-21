using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    //Set teleport target and location
    CharacterController controller;
    public Transform teleportTarget;
    public GameObject player;

    private void Start()
    {
        controller.enabled = true;
    }

    //Teleport the player when they collide
    void OnTriggerEnter(Collider player)
    {
        controller.enabled = false;
        player.transform.position = teleportTarget.transform.position;
        controller.enabled = true;
    }
}
