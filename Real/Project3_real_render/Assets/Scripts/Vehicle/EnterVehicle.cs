using UnityEngine;
using System.Collections;

public class EnterVehicle : MonoBehaviour
{
    private bool inVehicle = false;
    VehicleController vehicleScript;
    GameObject player;

    //Initialize Car
    void Start()
    {

    }

    //Enter Car
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                player.transform.parent = gameObject.transform;
                vehicleScript.enabled = true;
                player.SetActive(false);
                inVehicle = true;
            }
        }
    }
    void Update()
    {
        //vehicleScript = GetComponent<VehicleController>();
        player = GameObject.FindWithTag("Player");
        //Exit Vehicle
        if (inVehicle == true && Input.GetKey(KeyCode.E))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
        }
    }
}