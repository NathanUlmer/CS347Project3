using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanAnimation : MonoBehaviour
{
private Animator anim;
public AudioSource audsrc;
public bool isRunning, isLongSwing, isShortSwing, isIdle;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent <Animator> ();
        audsrc = GetComponent<AudioSource>();
        anim.SetBool("IsIdle", true);
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {


        //Short Sword Swing Animation
        if(Input.GetMouseButtonDown(0))
        {
            anim.CrossFade("Short Sword Swing", .1f);
        }
        if(Input.GetMouseButton(0))
        {
            isShortSwing = true;
            anim.SetBool("isShortSwing", true);
            anim.SetBool("IsIdle", false);

        }
        else
        {
            isShortSwing = false;
            anim.SetBool("isShortSwing", false);
        }

        //Long Sword Swing Animation
        if(Input.GetMouseButtonDown(1))
        {
            anim.CrossFade("Long Sword Swing", .1f);
        }

        if(Input.GetMouseButton(1))
        {
            isLongSwing = true;
            anim.SetBool("IsLongSwing", true);
            anim.SetBool("IsIdle", false);

        }
        else
        {
            isLongSwing = false;
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
            isRunning = true;
            anim.SetBool("isRunning", true);
            anim.SetBool("IsLongSwing", false);
            anim.SetBool("isShortSwing", false);
            anim.SetBool("IsIdle", false);
        }
        else
        {
            isRunning = false;
            isIdle = true;
            anim.SetBool("IsIdle", true);
            anim.SetBool("isRunning", false);
            audsrc.Stop();
        }
    }
}


