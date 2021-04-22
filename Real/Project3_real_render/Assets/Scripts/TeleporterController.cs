using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    //Set teleport target and location
    CharacterController controller;
    public Transform teleportTarget;

    private void Start()
    {
        //controller.enabled = true;
    }

    //Teleport the player when they collide
    void OnTriggerEnter(Collider other)
    {
        //controller.enabled = false;
        other.transform.position = teleportTarget.transform.position;
        //controller.enabled = true;
    }
}
