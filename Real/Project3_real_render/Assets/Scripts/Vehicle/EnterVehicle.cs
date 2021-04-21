using UnityEngine;
using System.Collections;

public class EnterVehicle : MonoBehaviour
{
    //Car Variables
    private bool inVehicle = false;
    VehicleController vehicleScript;
    SkinnedMeshRenderer playerRender;
    public GameObject guiObj;
    GameObject player;

    //Initialize Car
    void Start()
    {
        vehicleScript = GetComponent<VehicleController>();
        guiObj.SetActive(false);
        vehicleScript.enabled = false;
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            guiObj.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                guiObj.SetActive(false);
                player.transform.parent = other.gameObject.transform;
                vehicleScript.enabled = true;

                //Disable Character
                playerRender = GetComponentInChildren(typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;
                
                if(playerRender != null) playerRender.enabled = false;
                player.SetActive(false);
                inVehicle = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObj.SetActive(false);
        }
    }

    void Update()
    {
        //Find Player
        player = GameObject.FindWithTag("Player");

        //Exit Vehicle
        //You cannot
        /*if (inVehicle == true && Input.GetKey(KeyCode.F))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
        }*/
    }
}