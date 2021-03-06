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
        Debug.Log("Reached Camera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerTransform !=null)
        {
            //transform.position = playerTransform.position + new Vector3(0, headOffset, depth);
            transform.position = playerTransform.position + new Vector3(depth * Mathf.Sin(playerTransform.eulerAngles.y * Mathf.PI/180), headOffset, depth*Mathf.Cos(playerTransform.eulerAngles.y * Mathf.PI / 180));
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
        }
    }

    //Set the Camera Target
    public void setTarget(Transform target)
    {
        Debug.Log("Set Camera Target");
        playerTransform = target;
    }
}
