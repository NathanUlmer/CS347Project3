using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    //Set teleport Target and Location
    CharacterController controller;
    public Transform teleportTarget;

    //Teleport Object On Collision
    void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportTarget.transform.position;
    }
}
