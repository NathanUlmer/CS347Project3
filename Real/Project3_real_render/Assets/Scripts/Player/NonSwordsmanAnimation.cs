using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonSwordsmanAnimation : MonoBehaviour
{
    private Animator anim;
    public AudioSource audsrc;
    private bool inVehicle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent <Animator> ();
        audsrc = GetComponent<AudioSource>();
        anim.SetBool("IsIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(inVehicle == false)
        {
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
            }
            else
            {
                anim.SetBool("IsIdle", true);
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
        else
        {
            inVehicle = false;
        }
    }
}
