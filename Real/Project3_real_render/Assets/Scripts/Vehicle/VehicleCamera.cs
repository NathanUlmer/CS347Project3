using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float transSpeed;
    [SerializeField] private float rotSpeed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleTrans();
        HandleRot();
    }
    private void HandleTrans()
    {
        var targetPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, transSpeed * Time.deltaTime);
    }
    private void HandleRot()
    {
        var direction = target.position - transform.position;
        var rot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }
}
