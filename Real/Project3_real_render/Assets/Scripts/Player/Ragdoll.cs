using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
        GetComponent<SwordsmanAnimation>().enabled = true;
        GetComponent<PlayerObjectController>().enabled = true;
    }

    //Ragdoll On Death
    public void die()
    {
        GetComponent<SwordsmanAnimation>().enabled = false;
        GetComponent<PlayerObjectController>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        if (gameObject != null)
        {
            Destroy(gameObject, 3f);
        }
    }

    //Change Rigid Body State
    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    //Change Collider State
    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }
}