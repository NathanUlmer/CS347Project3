using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCastAbility : MonoBehaviour
{
    public int damage = 5;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            VehicleController vehicle = other.GetComponent<VehicleController>();
            if (vehicle != null)
            {
                vehicle.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
