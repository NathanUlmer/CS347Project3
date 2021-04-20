using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour
{

    public Transform playerTransform;
    public float depth = 0f;
    public float headOffset = 1f;
    float x=0f;
    float z=0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerTransform !=null)
        {
            transform.position = playerTransform.position + new Vector3(0, headOffset, depth);
            transform.rotation = playerTransform.rotation;

            float temp;
            temp = -Input.GetAxis("Mouse Y") * Time.deltaTime * 100.0f;
            z += temp;


            transform.eulerAngles += new Vector3(z, 0, 0);
            float minAngle =360f-60f;
            float maxAngle = 50f;


            if (transform.eulerAngles.x <= minAngle && transform.eulerAngles.x >= 180)
            {
                Debug.Log(transform.eulerAngles);
                transform.eulerAngles = new Vector3(minAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                z -= temp;
            }

            if (transform.eulerAngles.x >= maxAngle && transform.eulerAngles.x < 180)
            {
                Debug.Log(transform.eulerAngles);
                transform.eulerAngles = new Vector3(maxAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                z -= temp;
            }




            //transform.Translate(0, 0, z);
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}
