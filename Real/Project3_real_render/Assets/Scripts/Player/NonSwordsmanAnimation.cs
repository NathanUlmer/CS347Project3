using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NonSwordsmanAnimation : NetworkBehaviour
{
    private Animator anim;
    public AudioSource audsrc;
    private bool inVehicle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent <Animator> ();
        audsrc = GetComponent<AudioSource>();
        anim.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(inVehicle == false)
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                anim.CrossFade("Spawning", .2f);
            }
            if(Input.GetKey(KeyCode.T))
            {
                anim.SetBool("isSpawning", true);
            }

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {

                anim.CrossFade("Running", .1f);
                audsrc.Play();
            }
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {   
                anim.SetBool("isRunning", true);
                anim.SetBool("isDriver", false);
                anim.SetBool("isPassenger", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isGunner", false);
                anim.SetBool("isSpawning", false);
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
                audsrc.Stop();
            }


        }
    }

    //Enter Vehicle
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Vehicle" && Input.GetKey(KeyCode.E))
        {
            inVehicle = true;
            anim.SetBool("isDriver", true);
            anim.CrossFade("isDriver", .1f);
        }

    }
}
