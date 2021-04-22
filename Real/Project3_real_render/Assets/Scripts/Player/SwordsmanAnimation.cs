using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwordsmanAnimation : NetworkBehaviour
{
private Animator anim;
public AudioSource audsrc;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) return;
        anim = GetComponent <Animator> ();
        audsrc = GetComponent<AudioSource>();
        anim.SetBool("IsIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if(Input.GetKeyDown(KeyCode.T))
        {
            anim.CrossFade("Spawn", 0.2f);
        }
        if(Input.GetKey(KeyCode.T))
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("isShortSwing", false);
            anim.SetBool("IsLongSwing", false);    
            anim.SetBool("isSpawning", true);    
        }
        else
        {
            anim.SetBool("isSpawning", false);
        }

        //Short Sword Swing Animation
        if(Input.GetMouseButtonDown(0))
        {
            anim.CrossFade("Short Sword Swing", .1f);
        }
        if(Input.GetMouseButton(0))
        {
            anim.SetBool("isShortSwing", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("isSpawning", false);
        }
        else
        {
            anim.SetBool("isShortSwing", false);
        }

        //Long Sword Swing Animation
        if(Input.GetMouseButtonDown(1))
        {
            anim.CrossFade("Long Sword Swing", .1f);
        }
        if(Input.GetMouseButton(1))
        {
            anim.SetBool("IsLongSwing", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("isSpawning", false);
        }
        else
        {
            anim.SetBool("IsLongSwing", false);
        }

        //running animation
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            anim.CrossFade("Running", .1f);
            audsrc.Play();
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {   
            anim.SetBool("isRunning", true);
            anim.SetBool("IsLongSwing", false);
            anim.SetBool("isShortSwing", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("isSpawning", false);
        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("isRunning", false);
            audsrc.Stop();
        }
    }
}
